using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository OrganizationRepository;
    private readonly IBrregApiService BrregApiService;

    public OrganizationService(IOrganizationRepository organizationRepository, IBrregApiService brregApiService)
    {
        OrganizationRepository = organizationRepository;
        BrregApiService = brregApiService;
    }

    public async Task<OrganizationModel> Create(OrganizationNumber organisasjonsNummer, OrganizationModel model)
    {
        // NOTE: With async, or multiple requests, this doesn't protect against a race condition.
        // This mostly for a nice error message, but could probably be done better.
        await RequireNoOrganizationExists(organisasjonsNummer);

        // NOTE: Rate-limiting might be ideal, also potential attack point.
        var brregModel = await BrregApiService.GetOrganization(organisasjonsNummer);

        var organization = new Organization(organisasjonsNummer, brregModel);
        organization.UpdateFromModel(brregModel);
        organization.UpdateFromModel(model);

        await OrganizationRepository.Create(organization);

        return OrganizationModel.FromOrganization(organization);
    }

    public async Task<OrganizationModel> Get(OrganizationNumber organisasjonsNummer)
    {
        var organization = await RequireOrganization(organisasjonsNummer);
        return OrganizationModel.FromOrganization(organization);
    }

    public async Task<OrganizationModel> Update(OrganizationNumber organisasjonsNummer, OrganizationModel model)
    {
        var organization = await RequireOrganization(organisasjonsNummer);
        
        organization.UpdateFromModel(model);
        await OrganizationRepository.Update(organization);

        return OrganizationModel.FromOrganization(organization);
    }

    public async Task Delete(OrganizationNumber organisasjonsNummer)
    {
        var organization = await RequireOrganization(organisasjonsNummer);
        await OrganizationRepository.Delete(organization);
    }

    public async Task<OrganizationModel> Synchronize(OrganizationNumber organisasjonsNummer)
    {
        var organization = await RequireOrganization(organisasjonsNummer);

        var brregModel = await BrregApiService.GetOrganization(organisasjonsNummer);
        
        organization.SetSource(brregModel);
        organization.UpdateFromModel(brregModel);
        await OrganizationRepository.Update(organization);

        return OrganizationModel.FromOrganization(organization);
    }

    private async Task RequireNoOrganizationExists(OrganizationNumber organisasjonsNummer)
    {
        if(await OrganizationRepository.Get(organisasjonsNummer) != null)
        {
            throw new OrgDemoException(OrgDemoException.ErrorCode.OrganizationAlreadyExists);
        }
    }
    
    private async Task<Organization> RequireOrganization(OrganizationNumber organisasjonsNummer)
    {
        var organization = await OrganizationRepository.Get(organisasjonsNummer)
            ?? throw new OrgDemoException(OrgDemoException.ErrorCode.OrganizationDoesntExist);
        
        return organization!;
    }
}