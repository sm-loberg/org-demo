using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository OrganizationRepository;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        OrganizationRepository = organizationRepository;
    }

    public OrganizationModel Create(string organisasjonsNummer, OrganizationModel model)
    {
        RequireNoOrganizationExists(organisasjonsNummer);

        var organization = new Organization(organisasjonsNummer);
        organization.UpdateFromModel(model);

        OrganizationRepository.Create(organization);

        return OrganizationModel.FromOrganization(organization);
    }

    public OrganizationModel Get(string organisasjonsNummer)
    {
        var organization = RequireOrganization(organisasjonsNummer);

        return OrganizationModel.FromOrganization(organization);
    }

    public OrganizationModel Update(string organisasjonsNummer, OrganizationModel model)
    {
        var organization = RequireOrganization(organisasjonsNummer);
        
        organization.UpdateFromModel(model);
        OrganizationRepository.Update(organization);

        return OrganizationModel.FromOrganization(organization);
    }

    public void Delete(string organisasjonsNummer)
    {
        var organization = RequireOrganization(organisasjonsNummer);
        OrganizationRepository.Delete(organization);
    }

    public void Synchronize(string organisasjonsNummer)
    {
        throw new NotImplementedException();
    }

    private void RequireNoOrganizationExists(string organisasjonsNummer)
    {
        Error.Require(OrganizationRepository.Get(organisasjonsNummer) == null, OrgDemoException.ErrorCode.OrganizationAlreadyExists);
    }

    private Organization RequireOrganization(string organisasjonsNummer)
    {
        var organization = OrganizationRepository.Get(organisasjonsNummer);
        Error.Require(organization != null, OrgDemoException.ErrorCode.OrganizationDoesntExist);
        return organization!;
    }
}