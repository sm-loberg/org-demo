namespace OrgDemo.Logic;

public class LogicException : Exception
{
    public enum ErrorCode
    {
        OrganizationAlreadyExists
    }

    public ErrorCode Code { get; set; }

    public LogicException(ErrorCode code)
    {
        Code = code;
    }
}
