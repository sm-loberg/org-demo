using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public class BrregApiService : IBrregApiService
{
    private static readonly HttpClient HttpClient = new();
    private readonly string? GetOrganizationUrl;

    private class BrregJsonOrganisasjonsForm
    {
        public required string Kode { get; set; }
    };

    private class BrregJsonForretningsAdresse
    {
        public required List<string> Adresse { get; set; }
    }

    private class BrregJson
    {
        public required string Navn { get; set; }

        public int AntallAnsatte { get; set; }
        public DateOnly StiftelsesDato { get; set; }
        public required BrregJsonOrganisasjonsForm OrganisasjonsForm { get; set; }
        public required BrregJsonForretningsAdresse ForretningsAdresse { get; set; }
    };

    public BrregApiService(IConfiguration configuration)
    {
        // NOTE: Risk of null, should check valid configuration
        GetOrganizationUrl = configuration["Brreg:EnheterUrl"];
    }

    public async Task<OrganizationModel> GetOrganization(OrganizationNumber organisasjonsNummer)
    {
        // TODO: Stricter assurance of organissasjonsNummer to prevent possible arbitrary-string security issue
        var response = await HttpClient.GetAsync(GetOrganizationUrl + organisasjonsNummer.Value);
        Error.Require(response.IsSuccessStatusCode, OrgDemoException.ErrorCode.FailedToDownloadBrregOrganization);

        var json = await response.Content.ReadFromJsonAsync<BrregJson>()
            ?? throw new OrgDemoException(OrgDemoException.ErrorCode.FailedToParseBrregOrganization);

        return new OrganizationModel
        {
            Navn = json.Navn,
            Adresse = json.ForretningsAdresse.Adresse,
            AntallAnsatte = json.AntallAnsatte,
            Selskapsform = json.OrganisasjonsForm.Kode,
            StiftelsesDato = json.StiftelsesDato
        };
    }
}
