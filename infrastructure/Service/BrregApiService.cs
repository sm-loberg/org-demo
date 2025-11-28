using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public class BrregApiService : IBrregApiService
{
    private static readonly HttpClient HttpClient = new();
    private readonly string? GetOrganizationUrl;

    public BrregApiService(IConfiguration configuration)
    {
        // NOTE: Risk of null, should check valid configuration
        GetOrganizationUrl = configuration["Brreg:EnheterUrl"];
    }

    public async Task<BrregOrganizationResultModel> GetOrganization(string organisasjonsNummer)
    {
        // TODO: Stricter assurance of organissasjonsNummer to prevent possible arbitrary-string security issue
        var response = await HttpClient.GetAsync(GetOrganizationUrl + organisasjonsNummer);
        Error.Require(response.IsSuccessStatusCode, OrgDemoException.ErrorCode.FailedToDownloadBrregOrganization);

        return new BrregOrganizationResultModel
        {
            SourceJson = await response.Content.ReadAsStringAsync()
        };
    }
}
