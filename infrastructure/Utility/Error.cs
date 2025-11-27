namespace OrgDemo.Infrastructure;

public class Error
{
    public static void Require(bool mustBeTrue, OrgDemoException.ErrorCode code)
    {
        if(!mustBeTrue)
        {
            throw new OrgDemoException(code);
        }
    }
}