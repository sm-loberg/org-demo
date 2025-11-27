namespace OrgDemo.Logic;

public interface IOrganizationRepository
{
    public Organization Create();
    public Organization Get();
    public void Delete(Organization organization);

}
