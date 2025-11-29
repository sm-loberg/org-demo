namespace OrgDemo.Logic;

public interface IBrregApiService
{
    public Task<OrganizationModel> GetOrganization(OrganizationNumber organisasjonsNummer);
}
