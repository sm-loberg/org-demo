using OrgDemo.Logic;
using OrgDemo.System;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository OrganizationRepository;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        OrganizationRepository = organizationRepository;
    }

    public OrganizationModel Create(OrganizationModel model)
    {
        throw new NotImplementedException();
    }

    public void Delete(long id)
    {
        throw new NotImplementedException();
    }

    public OrganizationModel Get(long id)
    {
        throw new NotImplementedException();
    }

    public void Synchronize(long id)
    {
        throw new NotImplementedException();
    }

    public OrganizationModel Update(long id, OrganizationModel model)
    {
        throw new NotImplementedException();
    }
}