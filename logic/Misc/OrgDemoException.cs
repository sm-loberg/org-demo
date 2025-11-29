namespace OrgDemo.Logic;

public class OrgDemoException : Exception
{
    public enum ErrorCode
    {
        InternalError,
        OrganizationAlreadyExists,
        OrganizationDoesntExist,
        FailedToDownloadBrregOrganization,
        FailedToParseBrregOrganization,

        InvalidOrganizationNumberFormat
    }

    public ErrorCode Code { get; private set; }

    public OrgDemoException(ErrorCode code)
    {
        Code = code;
    }

    public ErrorModel ToModel()
    {
        return new ErrorModel
        {
            ErrorCode = Code.ToString()
        };
    }
}
