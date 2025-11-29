using System.Text.Json;
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

    private class OrganizationDownloadResult
    {
        public required Organization Organization;
        public required OrganizationModel DownloadModel;
    }

    private async Task<OrganizationDownloadResult> DownloadOrganization(Organization organization)
    {
        return new OrganizationDownloadResult
        {
            Organization = organization,
            DownloadModel = await BrregApiService.GetOrganization(new OrganizationNumber(organization))
        };
    }

    public async Task SynchronizeAll()
    {
        // Fire off all API requests in parallell
        // NOTE: Should probably rate limit here in a practical application
        List<Task<OrganizationDownloadResult>> downloadTasks = [];
        foreach(var organization in OrganizationRepository.ListAll())
        {
            downloadTasks.Add(DownloadOrganization(organization));
        }
        var results = await Task.WhenAll(downloadTasks);
        
        foreach(var data in results)
        {
            var changedModel = OrganizationModel.GetModified(data.Organization.GetSourceModel(), OrganizationModel.FromOrganization(data.Organization));

            data.Organization.UpdateFromModel(data.DownloadModel);
            data.Organization.UpdateFromModel(changedModel);
            data.Organization.SetSource(data.DownloadModel);
        }
        
        OrganizationRepository.UpdateAll(results.Select(x => x.Organization).ToList());
    }
}
