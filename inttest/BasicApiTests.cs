using System.Net.Http.Json;
using System.Reflection.Metadata;
using OrgDemo.Logic;

namespace OrgDemo.IntTest;

public class BasicApiTests : IClassFixture<ApiFactory<Program>>
{
    private readonly ApiFactory<Program> Factory;

    public BasicApiTests(ApiFactory<Program> factory)
    {
        Factory = factory;
    }

    [Theory]
    [InlineData("977047838", OrgDemoException.ErrorCode.OrganizationDoesntExist)]
    [InlineData("123123123", OrgDemoException.ErrorCode.InvalidOrganizationNumberFormat)]
    public async Task GetErrorHandling(string orgNo, OrgDemoException.ErrorCode code)
    {
        var client = Factory.CreateClient();

        var response = await client.GetAsync($"api/v1/organization/{orgNo}");
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equivalent(
            new ErrorModel
            {
                ErrorCode = code.ToString()
            },
            await response.Content.ReadFromJsonAsync<ErrorModel>()
        );
    }

    [Theory]
    [InlineData("977047838", OrgDemoException.ErrorCode.OrganizationDoesntExist)]
    [InlineData("911111111", OrgDemoException.ErrorCode.OrganizationDoesntExist)]
    [InlineData("123123123", OrgDemoException.ErrorCode.InvalidOrganizationNumberFormat)]
    public async Task UpdateErrorHandling(string orgNo, OrgDemoException.ErrorCode code)
    {
        var client = Factory.CreateClient();

        var response = await client.PostAsync(
            $"api/v1/organization/{orgNo}",
            JsonContent.Create(new OrganizationModel {})
        );
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equivalent(
            new ErrorModel
            {
                ErrorCode = code.ToString()
            },
            await response.Content.ReadFromJsonAsync<ErrorModel>()
        );
    }

    [Theory]
    [InlineData("911111111", OrgDemoException.ErrorCode.FailedToDownloadBrregOrganization)]
    [InlineData("123123123", OrgDemoException.ErrorCode.InvalidOrganizationNumberFormat)]
    public async Task CreateErrorHandling(string orgNo, OrgDemoException.ErrorCode code)
    {
        var client = Factory.CreateClient();

        var response = await client.PostAsync(
            $"api/v1/organization/{orgNo}/create",
            JsonContent.Create(new OrganizationModel {})
        );
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equivalent(
            new ErrorModel
            {
                ErrorCode = code.ToString()
            },
            await response.Content.ReadFromJsonAsync<ErrorModel>()
        );
    }

    [Theory]
    [InlineData("911111111", OrgDemoException.ErrorCode.OrganizationDoesntExist)]
    [InlineData("123123123", OrgDemoException.ErrorCode.InvalidOrganizationNumberFormat)]
    public async Task DeleteErrorHandling(string orgNo, OrgDemoException.ErrorCode code)
    {
        var client = Factory.CreateClient();

        var response = await client.DeleteAsync($"api/v1/organization/{orgNo}");
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equivalent(
            new ErrorModel
            {
                ErrorCode = code.ToString()
            },
            await response.Content.ReadFromJsonAsync<ErrorModel>()
        );
    }

    [Theory]
    [InlineData("911111111", OrgDemoException.ErrorCode.OrganizationDoesntExist)]
    [InlineData("123123123", OrgDemoException.ErrorCode.InvalidOrganizationNumberFormat)]
    public async Task SynchronizeErrorHandling(string orgNo, OrgDemoException.ErrorCode code)
    {
        var client = Factory.CreateClient();

        var response = await client.GetAsync($"api/v1/organization/{orgNo}/synchronize");
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equivalent(
            new ErrorModel
            {
                ErrorCode = code.ToString()
            },
            await response.Content.ReadFromJsonAsync<ErrorModel>()
        );
    }

    public static List<object[]> TestNormalFlowData => new()
    {
        new object[] {
            "977047838",
            new OrganizationModel
            {
                Adresse = ["A", "B", "C"],
                AntallAnsatte = 5
            },
            new OrganizationModel
            {
                Navn = "POWER NORGE AS",
                Adresse = ["A", "B", "C"],
                AntallAnsatte = 5,
                Selskapsform = "AS",
                StiftelsesDato = new DateOnly(1996, 12, 9)
            }
        },
        new object[] {
            "977047838",
            new OrganizationModel
            {
            },
            new OrganizationModel
            {
                Navn = "POWER NORGE AS",
                Adresse = ["Solheimveien 6"],
                AntallAnsatte = 2060,
                Selskapsform = "AS",
                StiftelsesDato = new DateOnly(1996, 12, 9)
            }
        },
        new object[] {
            "977047838",
            new OrganizationModel
            {
                Navn = "A",
                Adresse = [],
                AntallAnsatte = 1,
                Selskapsform = "XY",
                StiftelsesDato = new DateOnly(1996, 12, 8)
            },
            new OrganizationModel
            {
                Navn = "A",
                Adresse = [],
                AntallAnsatte = 1,
                Selskapsform = "XY",
                StiftelsesDato = new DateOnly(1996, 12, 8)
            }
        }
    };

    [Theory]
    [MemberData(nameof(TestNormalFlowData))]
    public async Task TestNormalFlow(
        string orgNo,
        OrganizationModel firstCreate,
        OrganizationModel firstCreateResult
    )
    {
        var client = Factory.CreateClient();

        var createResponse = await client.PostAsync(
            $"api/v1/organization/{orgNo}/create",
            JsonContent.Create(firstCreate)
        );
        createResponse.EnsureSuccessStatusCode();
        Assert.Equivalent(
            firstCreateResult,
            await createResponse.Content.ReadFromJsonAsync<OrganizationModel>()
        );

        var deleteResponse = await client.DeleteAsync($"api/v1/organization/{orgNo}");
        deleteResponse.EnsureSuccessStatusCode();
    }
}
