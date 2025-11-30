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

    private static readonly OrganizationModel OriginalPower = new()
    {
        Navn = "POWER NORGE AS",
        Adresse = ["Solheimveien 6"],
        AntallAnsatte = 2060,
        Selskapsform = "AS",
        StiftelsesDato = new DateOnly(1996, 12, 9)
    };

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
            },
            new OrganizationModel
            {
                Navn = "POWER"
            },
            new OrganizationModel
            {
                Navn = "POWER",
                Adresse = ["A", "B", "C"],
                AntallAnsatte = 5,
                Selskapsform = "AS",
                StiftelsesDato = new DateOnly(1996, 12, 9)
            },
            OriginalPower
        },
        new object[] {
            "977047838",
            new OrganizationModel {},
            OriginalPower,
            OriginalPower,
            OriginalPower,
            OriginalPower
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
            },
            new OrganizationModel {},
            new OrganizationModel
            {
                Navn = "A",
                Adresse = [],
                AntallAnsatte = 1,
                Selskapsform = "XY",
                StiftelsesDato = new DateOnly(1996, 12, 8)
            },
            OriginalPower
        }
    };

    [Theory]
    [MemberData(nameof(TestNormalFlowData))]
    public async Task TestNormalFlow(
        string orgNo,
        OrganizationModel createData,
        OrganizationModel createResult,
        OrganizationModel updateData,
        OrganizationModel updateResult,
        OrganizationModel syncResult
    )
    {
        var client = Factory.CreateClient();

        var createResponse = await client.PostAsync(
            $"api/v1/organization/{orgNo}/create",
            JsonContent.Create(createData)
        );
        createResponse.EnsureSuccessStatusCode();
        Assert.Equivalent(
            createResult,
            await createResponse.Content.ReadFromJsonAsync<OrganizationModel>()
        );

        var updateResponse = await client.PostAsync(
            $"api/v1/organization/{orgNo}",
            JsonContent.Create(updateData)
        );
        updateResponse.EnsureSuccessStatusCode();
        Assert.Equivalent(
            updateResult,
            await updateResponse.Content.ReadFromJsonAsync<OrganizationModel>()
        );

        var getResponse = await client.GetAsync($"api/v1/organization/{orgNo}");
        getResponse.EnsureSuccessStatusCode();
        Assert.Equivalent(
            updateResult,
            await getResponse.Content.ReadFromJsonAsync<OrganizationModel>()
        );

        var syncResponse = await client.GetAsync(
            $"api/v1/organization/{orgNo}/synchronize"
        );
        syncResponse.EnsureSuccessStatusCode();
        Assert.Equivalent(
            syncResult,
            await syncResponse.Content.ReadFromJsonAsync<OrganizationModel>()
        );

        var deleteResponse = await client.DeleteAsync($"api/v1/organization/{orgNo}");
        deleteResponse.EnsureSuccessStatusCode();
    }
}
