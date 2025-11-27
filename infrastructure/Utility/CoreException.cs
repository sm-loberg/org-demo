namespace OrgDemo.Infrastructure;

public class OrgDemoException : Exception
{
    public enum ErrorCode
    {
        InternalError,
        OrganizationAlreadyExists,
        OrganizationDoesntExist
    }

    public ErrorCode Code { get; set; }

    public OrgDemoException(ErrorCode code)
    {
        Code = code;
    }
}
