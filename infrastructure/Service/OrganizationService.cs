using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository OrganizationRepository;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        OrganizationRepository = organizationRepository;
    }

    public OrganizationModel Create(OrganizationModel model)
    {
        Error.Require(OrganizationRepository.Get(model.OrganisasjonsNummer) == null, OrgDemoException.ErrorCode.OrganizationAlreadyExists);

        var organization = OrganizationRepository.Create();

        throw new NotImplementedException();
    }

    public void Delete(string organisasjonsNummer)
    {
        throw new NotImplementedException();
    }

    public OrganizationModel Get(string organisasjonsNummer)
    {
        throw new NotImplementedException();
    }

    public void Synchronize(string organisasjonsNummer)
    {
        throw new NotImplementedException();
    }

    public OrganizationModel Update(string organisasjonsNummer, OrganizationModel model)
    {
        throw new NotImplementedException();
    }
}