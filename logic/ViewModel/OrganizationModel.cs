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

    public static OrganizationModel GetModified(OrganizationModel original, OrganizationModel current)
    {
        return new OrganizationModel
        {
            Navn =              original.Navn != current.Navn                       ? current.Navn : null,
            Adresse = original.Adresse != null && current.Adresse != null
                && !Enumerable.SequenceEqual(original.Adresse, current.Adresse)     ? current.Adresse : null,
            AntallAnsatte =     original.AntallAnsatte != current.AntallAnsatte     ? current.AntallAnsatte : null,
            Selskapsform =      original.Selskapsform != current.Selskapsform       ? current.Selskapsform : null,
            StiftelsesDato =    original.StiftelsesDato != current.StiftelsesDato   ? current.StiftelsesDato : null
        };
    }
}
