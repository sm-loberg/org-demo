namespace OrgDemo.Logic;

public interface IBrregApiService
{
    public Task<BrregOrganizationResultModel> GetOrganization(string organisasjonsNummer);
}
