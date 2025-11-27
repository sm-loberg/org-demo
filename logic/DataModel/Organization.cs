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
}