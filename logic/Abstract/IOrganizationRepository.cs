namespace OrgDemo.Logic;

public interface IOrganizationRepository
{
    public void Create(Organization organization);
    public Organization? Get(OrganizationNumber organisasjonsNummer);
    public void Update(Organization organization);
    public void Delete(Organization organization);

    public List<Organization> ListAll();
    public void UpdateAll(List<Organization> organizations);
}
