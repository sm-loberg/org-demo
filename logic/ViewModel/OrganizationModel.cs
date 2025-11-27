namespace OrgDemo.Logic;

public class OrganizationModel
{
    public required string OrganisasjonsNummer { get; set; }
    public int AntallAnsatte { get; set; }
    public required string Selskapsform { get; set; }
    public DateOnly StiftelsesDato { get; set; }
}