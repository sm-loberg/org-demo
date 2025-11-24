namespace OrgDemo.Logic;

public class OrganizationModel
{
    public string OrganisasjonsNummer { get; set; }
    public int AntallAnsatte { get; set; }
    public string Selskapsform { get; set; }
    public DateOnly StiftelsesDato { get; set; }
}