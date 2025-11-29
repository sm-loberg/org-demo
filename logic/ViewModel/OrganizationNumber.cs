using System.Text.RegularExpressions;

namespace OrgDemo.Logic;

public class OrganizationNumber
{
    public string Value { get; }

    private OrganizationNumber(string value)
    {
        Value = value;
    }

    public OrganizationNumber(Organization organization)
        : this(organization.OrganisasjonsNummer)
    {
    }

    public static OrganizationNumber FromString(string value)
    {
        if(!Regex.IsMatch(value, "[89][0-9]{8}"))
        {
            throw new LogicException(LogicException.ErrorCode.InvalidOrganizationNumberFormat);
        }

        return new OrganizationNumber(value);
    }
}
