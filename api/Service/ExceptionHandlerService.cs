using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using OrgDemo.Logic;

public class ExceptionHandlerService : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is OrgDemoException baseException)
        {
            ErrorModel errorModel = baseException.ToModel();
        
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await httpContext.Response.WriteAsJsonAsync(errorModel, cancellationToken);
            return true;
        }

        return false;
    }
}
