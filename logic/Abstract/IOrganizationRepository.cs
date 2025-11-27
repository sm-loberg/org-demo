namespace OrgDemo.Logic;

public interface IOrganizationRepository
{
    public Organization Create(string organisasjonsNummer);
    public Organization? Get(string organisasjonsNummer);
    public void Delete(Organization organization);

}
