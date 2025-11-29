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

    public OrganizationModel Create(OrganizationNumber organisasjonsNummer, OrganizationModel model)
    {
        RequireNoOrganizationExists(organisasjonsNummer);

        var organization = new Organization(organisasjonsNummer);

        var brregModel = BrregApiService.GetOrganization(organisasjonsNummer).Result;

        organization.UpdateFromModel(brregModel);
        organization.UpdateFromModel(model);

        OrganizationRepository.Create(organization);

        return OrganizationModel.FromOrganization(organization);
    }

    public OrganizationModel Get(OrganizationNumber organisasjonsNummer)
    {
        var organization = RequireOrganization(organisasjonsNummer);

        return OrganizationModel.FromOrganization(organization);
    }

    public OrganizationModel Update(OrganizationNumber organisasjonsNummer, OrganizationModel model)
    {
        var organization = RequireOrganization(organisasjonsNummer);
        
        organization.UpdateFromModel(model);
        OrganizationRepository.Update(organization);

        return OrganizationModel.FromOrganization(organization);
    }

    public void Delete(OrganizationNumber organisasjonsNummer)
    {
        var organization = RequireOrganization(organisasjonsNummer);
        OrganizationRepository.Delete(organization);
    }

    public OrganizationModel Synchronize(OrganizationNumber organisasjonsNummer)
    {
        var organization = RequireOrganization(organisasjonsNummer);

        var brregModel = BrregApiService.GetOrganization(organisasjonsNummer).Result;

        organization.UpdateFromModel(brregModel);
        OrganizationRepository.Update(organization);

        return OrganizationModel.FromOrganization(organization);
    }

    private void RequireNoOrganizationExists(OrganizationNumber organisasjonsNummer)
    {
        Error.Require(OrganizationRepository.Get(organisasjonsNummer) == null, OrgDemoException.ErrorCode.OrganizationAlreadyExists);
    }

    private Organization RequireOrganization(OrganizationNumber organisasjonsNummer)
    {
        var organization = OrganizationRepository.Get(organisasjonsNummer);
        Error.Require(organization != null, OrgDemoException.ErrorCode.OrganizationDoesntExist);
        return organization!;
    }
}