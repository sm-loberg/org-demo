namespace OrgDemo.Logic;

public interface IOrganizationService
{
    public OrganizationModel Create(OrganizationModel model);
    public OrganizationModel Get(long id);
    public OrganizationModel Update(long id, OrganizationModel model);
    public void Delete(long id);

    public void Synchronize(long id);
}
