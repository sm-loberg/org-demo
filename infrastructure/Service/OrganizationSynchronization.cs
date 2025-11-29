using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public class OrganizationSynchronization : IOrganizationSynchronization
{
    private readonly IOrganizationRepository OrganizationRepository;
    private readonly IBrregApiService BrregApiService;

    public OrganizationSynchronization(IOrganizationRepository organizationRepository, IBrregApiService brregApiService)
    {
        OrganizationRepository = organizationRepository;
        BrregApiService = brregApiService;
    }

    public void SynchronizeAll()
    {
        throw new NotImplementedException();
    }
}