using OrgDemo.Infrastructure;
using OrgDemo.Logic;

namespace OrgDemo.IntTest;

public class BrregApiServiceMock : IBrregApiService
{
    public Dictionary<string, OrganizationModel> MockData = new Dictionary<string, OrganizationModel>
    {
        {
            "919300388",
            new OrganizationModel
            {
                Navn = "GLASSPAPER SOLUTIONS AS",
                Adresse = ["Brynsveien 12"],
                AntallAnsatte = 10,
                Selskapsform = "AS",
                StiftelsesDato = new DateOnly(2017, 7, 17)
            }
        },
        {
            "979642121",
            new OrganizationModel
            {
                Navn = "KOMPLETT SERVICES AS",
                Adresse = ["Østre Kullerød 4"],
                AntallAnsatte = 295,
                Selskapsform = "AS",
                StiftelsesDato = new DateOnly(1998, 1, 1)
            }
        },
        {
            "977047838",
            new OrganizationModel
            {
                Navn = "POWER NORGE AS",
                Adresse = ["Solheimveien 6"],
                AntallAnsatte = 2060,
                Selskapsform = "AS",
                StiftelsesDato = new DateOnly(1996, 12, 9)
            }
        },
    };

    public void ReplaceMockData(OrganizationNumber orgNr, OrganizationModel newModel)
    {
        MockData[orgNr.Value] = newModel;
    }

    public Task<OrganizationModel> GetOrganization(OrganizationNumber organisasjonsNummer)
    {
        var foundValue = MockData.GetValueOrDefault(organisasjonsNummer.Value)
            ?? throw new OrgDemoException(OrgDemoException.ErrorCode.FailedToDownloadBrregOrganization);
        
        return Task.FromResult(foundValue);
    }
}
