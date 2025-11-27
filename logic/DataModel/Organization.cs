namespace OrgDemo.Logic;

public class Organization
{
    public string OrganisasjonsNummer { get; private set; } = "";
    public int AntallAnsatte { get; private set; } = 0;
    public string Selskapsform { get; private set; } = "";
    public DateOnly StiftelsesDato { get; private set; } = DateOnly.MinValue;

    public string SourceJson { get; private set; } = "";
    public DateTime CreatedAt { get; private set; } = DateTime.MinValue;
    public DateTime UpdatedAt { get; private set; } = DateTime.MinValue;

    public Organization()
    {
    }

    public Organization(string organisasjonsNummer)
    {
        OrganisasjonsNummer = organisasjonsNummer;
        
        CreatedAt = UpdatedAt = DateTime.UtcNow;
    }

    private void MarkUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateFromModel(OrganizationModel model)
    {
        AntallAnsatte = model.AntallAnsatte ?? AntallAnsatte;
        Selskapsform = model.Selskapsform ?? Selskapsform;
        StiftelsesDato = model.StiftelsesDato ?? StiftelsesDato;

        MarkUpdated();
    }
}