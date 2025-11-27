namespace OrgDemo.Logic;

public class OrganizationModel
{
    public int? AntallAnsatte { get; set; }
    public string? Selskapsform { get; set; }
    public DateOnly? StiftelsesDato { get; set; }

    public static OrganizationModel FromOrganization(Organization organization)
    {
        return new OrganizationModel
        {
            AntallAnsatte = organization.AntallAnsatte,
            Selskapsform = organization.Selskapsform,
            StiftelsesDato = organization.StiftelsesDato
        };
    }
}