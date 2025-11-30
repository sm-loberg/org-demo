using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public interface IOrganizationService
{
    public Task<OrganizationModel> Create(OrganizationNumber organisasjonsNummer, OrganizationModel model);
    public Task<OrganizationModel> Get(OrganizationNumber organisasjonsNummer);
    public Task<OrganizationModel> Update(OrganizationNumber organisasjonsNummer, OrganizationModel model);
    public Task Delete(OrganizationNumber organisasjonsNummer);

    public Task<OrganizationModel> Synchronize(OrganizationNumber organisasjonsNummer);
}
