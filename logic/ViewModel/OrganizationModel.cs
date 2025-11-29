namespace OrgDemo.Logic;

public class OrganizationModel
{
    public string? Navn { get; set; }
    public List<string>? Adresse { get; set; }
    public int? AntallAnsatte { get; set; }
    public string? Selskapsform { get; set; }
    public DateOnly? StiftelsesDato { get; set; }

    public static OrganizationModel FromOrganization(Organization organization)
    {
        return new OrganizationModel
        {
            Navn = organization.Navn,
            Adresse = organization.Adresse,
            AntallAnsatte = organization.AntallAnsatte,
            Selskapsform = organization.Selskapsform,
            StiftelsesDato = organization.StiftelsesDato
        };
    }
}