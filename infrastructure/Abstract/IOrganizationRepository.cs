using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public interface IOrganizationRepository
{
    public Task Create(Organization organization);
    public Task<Organization?> Get(OrganizationNumber organisasjonsNummer);
    public Task Update(Organization organization);
    public Task Delete(Organization organization);

    public Task<List<Organization>> ListAll();
    public Task UpdateAll(List<Organization> organizations);
}
