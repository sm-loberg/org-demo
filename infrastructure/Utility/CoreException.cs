namespace OrgDemo.Infrastructure;

public class OrgDemoException : Exception
{
    public enum ErrorCode
    {
        InternalError,
        OrganizationAlreadyExists,
        OrganizationDoesntExist,
        FailedToDownloadBrregOrganization
    }

    public ErrorCode Code { get; set; }

    public OrgDemoException(ErrorCode code)
    {
        Code = code;
    }
}
