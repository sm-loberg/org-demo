using OrgDemo.Logic;

namespace OrgDemo.UnitTest;

public class TestOrganizationModel
{
    [Fact]
    public void TestFromString()
    {
        var testOrganization = new Organization(
            OrganizationNumber.FromString("897897897"),
            new OrganizationModel
            {
                Navn = "Test",
                Adresse = ["A", "B"],
                AntallAnsatte = 5,
                Selskapsform = "Test",
                StiftelsesDato = new DateOnly(2025, 5, 3)
            }
        );

        Assert.Equivalent(new OrganizationModel
        {
            Navn = testOrganization.Navn,
            Adresse = testOrganization.Adresse,
            AntallAnsatte = testOrganization.AntallAnsatte,
            Selskapsform = testOrganization.Selskapsform,
            StiftelsesDato = testOrganization.StiftelsesDato
        }, OrganizationModel.FromOrganization(testOrganization));
    }

    public static List<object[]> TestOrganizationModelChanged => new()
    {
        new object[] {
            new OrganizationModel
            {
                Navn = null,
                Adresse = null,
                AntallAnsatte = null,
                Selskapsform = null,
                StiftelsesDato = null
            },
            new OrganizationModel
            {
                Navn = null,
                Adresse = null,
                AntallAnsatte = null,
                Selskapsform = null,
                StiftelsesDato = null
            },
            new OrganizationModel
            {
                Navn = null,
                Adresse = null,
                AntallAnsatte = null,
                Selskapsform = null,
                StiftelsesDato = null
            }
        },
        new object[] {
            new OrganizationModel
            {
                Navn = "Original",
                Adresse = ["A", "B"],
                AntallAnsatte = 1,
                Selskapsform = "Org",
                StiftelsesDato = new DateOnly(2025, 1, 1)
            },
            new OrganizationModel
            {
                Navn = null,
                Adresse = null,
                AntallAnsatte = null,
                Selskapsform = null,
                StiftelsesDato = null
            },
            new OrganizationModel
            {
                Navn = null,
                Adresse = null,
                AntallAnsatte = null,
                Selskapsform = null,
                StiftelsesDato = null
            },
        },
        new object[] {
            new OrganizationModel
            {
                Navn = "KOMPLETT SERVICES AS",
                Adresse = ["Østre Kullerød 4"],
                AntallAnsatte = 295,
                Selskapsform = "AS",
                StiftelsesDato = new DateOnly(1998, 1, 1)
            },
            new OrganizationModel
            {
                Navn = "Custom Services AS",
                Adresse = ["Østre Kullerød 4"],
                AntallAnsatte = 295,
                Selskapsform = "AS",
                StiftelsesDato = new DateOnly(1998, 1, 1)
            },
            new OrganizationModel
            {
                Navn = "Custom Services AS",
                Adresse = null,
                AntallAnsatte = null,
                Selskapsform = null,
                StiftelsesDato = null
            },
        },
        new object[] {
            new OrganizationModel
            {
                Navn = null,
                Adresse = null,
                AntallAnsatte = null,
                Selskapsform = null,
                StiftelsesDato = null
            },
            new OrganizationModel
            {
                Navn = "KOMPLETT SERVICES AS",
                Adresse = ["Østre Kullerød 4"],
                AntallAnsatte = 295,
                Selskapsform = "AS",
                StiftelsesDato = new DateOnly(1998, 1, 1)
            },
            new OrganizationModel
            {
                Navn = "KOMPLETT SERVICES AS",
                Adresse = ["Østre Kullerød 4"],
                AntallAnsatte = 295,
                Selskapsform = "AS",
                StiftelsesDato = new DateOnly(1998, 1, 1)
            },
        },
        new object[] {
            new OrganizationModel { Adresse = null },
            new OrganizationModel { Adresse = null },
            new OrganizationModel { Adresse = null },
        },
        new object[] {
            new OrganizationModel { Adresse = ["Østre Kullerød 4"] },
            new OrganizationModel { Adresse = null },
            new OrganizationModel { Adresse = null },
        },
        new object[] {
            new OrganizationModel { Adresse = null },
            new OrganizationModel { Adresse = ["Østre Kullerød 4"] },
            new OrganizationModel { Adresse = ["Østre Kullerød 4"] },
        },
        new object[] {
            new OrganizationModel { Adresse = ["Østre Kullerød 4"] },
            new OrganizationModel { Adresse = ["Vestre Kullerød 6"] },
            new OrganizationModel { Adresse = ["Vestre Kullerød 6"] },
        },
        new object[] {
            new OrganizationModel { Adresse = ["V1", "V2"] },
            new OrganizationModel { Adresse = ["V1", "V2"] },
            new OrganizationModel { Adresse = null },
        },
        new object[] {
            new OrganizationModel { Adresse = ["V1", "V2"] },
            new OrganizationModel { Adresse = ["V1"] },
            new OrganizationModel { Adresse = ["V1"] },
        },
    };

    [Theory]
    [MemberData(nameof(TestOrganizationModelChanged))]
    public void TestModified(OrganizationModel original, OrganizationModel modified, OrganizationModel expected)
    {
        Assert.Equivalent(
            expected,
            OrganizationModel.GetModified(original, modified)
        );
    }
}
