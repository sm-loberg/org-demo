namespace OrgDemo.Logic;

public interface IOrganizationRepository
{
    public Organization Create();
    public Organization Get(string organisasjonsNummer);
    public void Delete(Organization organization);

}
