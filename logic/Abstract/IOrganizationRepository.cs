namespace OrgDemo.Logic;

public interface IOrganizationRepository
{
    public void Create(Organization organization);
    public Organization? Get(string organisasjonsNummer);
    public void Update(Organization organization);
    public void Delete(Organization organization);

}
