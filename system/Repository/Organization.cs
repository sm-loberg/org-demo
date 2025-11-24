namespace OrgDemo.System;

public class Organization
{
    public string OrganisasjonsNummer { get; private set; }
    public int AntallAnsatte { get; private set; }
    public string Selskapsform { get; private set; }
    public DateOnly StiftelsesDato { get; private set; }

    public string SourceJson { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Organization()
    {
    }
}