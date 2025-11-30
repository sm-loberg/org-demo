using Microsoft.Extensions.DependencyInjection;
using OrgDemo.Infrastructure;
using OrgDemo.Logic;

namespace OrgDemo.IntTest;

public class SynchronizationTests : IClassFixture<SynchronizationFactory<Program>>
{
    private readonly SynchronizationFactory<Program> Factory;

    public SynchronizationTests(SynchronizationFactory<Program> factory)
    {
        Factory = factory;
    }

    [Fact]
    public async Task TestSynchronization()
    {
        using var serviceScope = Factory.Services.CreateScope();

        var orgService = serviceScope.ServiceProvider.GetRequiredService<IOrganizationService>();
        var syncService = serviceScope.ServiceProvider.GetRequiredService<IOrganizationSynchronization>()!;
        var brregMock = (BrregApiServiceMock)serviceScope.ServiceProvider.GetRequiredService<IBrregApiService>();

        var orgNr = OrganizationNumber.FromString("979642121");

        // Create organization and make sure it synchronizes normally
        await orgService.Create(orgNr, new OrganizationModel{});
        await syncService.SynchronizeAll();
        Assert.Equivalent(
            new OrganizationModel
            {
                Navn = "KOMPLETT SERVICES AS",
                Adresse = ["Østre Kullerød 4"],
                AntallAnsatte = 295,
                Selskapsform = "AS",
                StiftelsesDato = new DateOnly(1998, 1, 1)
            },
            await orgService.Get(orgNr)
        );

        // Test changed data from Brønnøysund
        await orgService.Update(orgNr, new OrganizationModel
        {
            Navn = "Custom Services AS"
        });
        Assert.Equivalent(
            new OrganizationModel
            {
                Navn = "Custom Services AS",
                Adresse = ["Østre Kullerød 4"],
                AntallAnsatte = 295,
                Selskapsform = "AS",
                StiftelsesDato = new DateOnly(1998, 1, 1)
            },
            await orgService.Get(orgNr)
        );
        brregMock.ReplaceMockData(orgNr, new OrganizationModel
        {
            Navn = "KOMPLETT SERVICES AS",
            Adresse = ["Vestre Kullerød 6"],
            AntallAnsatte = 305,
            Selskapsform = "AS",
            StiftelsesDato = new DateOnly(1998, 1, 1)
        });
        await syncService.SynchronizeAll();
        Assert.Equivalent(
            new OrganizationModel
            {
                Navn = "Custom Services AS",
                Adresse = ["Vestre Kullerød 6"],
                AntallAnsatte = 305,
                Selskapsform = "AS",
                StiftelsesDato = new DateOnly(1998, 1, 1)
            },
            await orgService.Get(orgNr)
        );

        // Test some more changed fields
        await orgService.Update(orgNr, new OrganizationModel
        {
            AntallAnsatte = 75,
            Adresse = ["59°10'37.1\"N", "10°13'04.6\"E"],
        });
        brregMock.ReplaceMockData(orgNr, new OrganizationModel
        {
            Navn = "KOMPLETTERE SERVICES AS",
            Adresse = ["Nordre Kullerød 1"],
            AntallAnsatte = 999,
            Selskapsform = "ES",
            StiftelsesDato = new DateOnly(1999, 1, 1)
        });
        await syncService.SynchronizeAll();
        Assert.Equivalent(
            new OrganizationModel
            {
                Navn = "Custom Services AS",
                Adresse = ["59°10'37.1\"N", "10°13'04.6\"E"],
                AntallAnsatte = 75,
                Selskapsform = "ES",
                StiftelsesDato = new DateOnly(1999, 1, 1)
            },
            await orgService.Get(orgNr)
        );
    }
}
