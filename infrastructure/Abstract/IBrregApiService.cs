using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public interface IBrregApiService
{
    public Task<OrganizationModel> GetOrganization(OrganizationNumber organisasjonsNummer);
}
