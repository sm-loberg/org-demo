using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public class BrregApiService : IBrregApiService
{
    public BrregApiService()
    {
    }

    public async Task<BrregOrganizationResultModel> GetOrganization(string organisasjonsNummer)
    {
        throw new NotImplementedException();
    }
}
