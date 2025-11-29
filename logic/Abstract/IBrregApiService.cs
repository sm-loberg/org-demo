namespace OrgDemo.Logic;

public interface IBrregApiService
{
    public Task<OrganizationModel> GetOrganization(string organisasjonsNummer);
}
