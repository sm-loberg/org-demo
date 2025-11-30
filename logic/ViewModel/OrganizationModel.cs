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

    private static T? GetChangedValue<T>(T? original, T? current)
    {
        return EqualityComparer<T>.Default.Equals(original, current)
            ? default
            : current;
    }

    private static List<string>? GetChangedSequence(List<string>? original, List<string>? current)
    {
        if(original != null && current != null)
        {
            return Enumerable.SequenceEqual(original, current) ? null : current;
        }
        return current;
    }

    public static OrganizationModel GetModified(OrganizationModel original, OrganizationModel current)
    {
        return new OrganizationModel
        {
            Navn =              GetChangedValue(original.Navn, current.Navn),
            Adresse =           GetChangedSequence(original.Adresse, current.Adresse),
            AntallAnsatte =     GetChangedValue(original.AntallAnsatte, current.AntallAnsatte),
            Selskapsform =      GetChangedValue(original.Selskapsform, current.Selskapsform),
            StiftelsesDato =    GetChangedValue(original.StiftelsesDato, current.StiftelsesDato)
        };
    }
}
