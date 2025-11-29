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

    [Fact]
    public void TestModified()
    {
        Assert.Equivalent(
            new OrganizationModel
            {
                Navn = null,
                Adresse = null,
                AntallAnsatte = null,
                Selskapsform = null,
                StiftelsesDato = null
            },
            OrganizationModel.GetModified(
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
            )
        );

        Assert.Equivalent(
            new OrganizationModel
            {
                Navn = "Original",
                Adresse = ["A", "B"],
                AntallAnsatte = 1,
                Selskapsform = "Org",
                StiftelsesDato = new DateOnly(2025, 1, 1)
            },
            OrganizationModel.GetModified(
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
                }
            )
        );

        Assert.Equivalent(
            new OrganizationModel
            {
                Navn = "Changed",
                Adresse = ["C"],
                AntallAnsatte = 1,
                Selskapsform = "Org",
                StiftelsesDato = new DateOnly(2025, 1, 1)
            },
            OrganizationModel.GetModified(
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
                    Navn = "Changed",
                    Adresse = ["C"],
                    AntallAnsatte = null,
                    Selskapsform = null,
                    StiftelsesDato = null
                }
            )
        );

        Assert.Equivalent(
            new OrganizationModel
            {
                Navn = "Changed",
                Adresse = ["C", "H", "A"],
                AntallAnsatte = 2,
                Selskapsform = "Other",
                StiftelsesDato = new DateOnly(2025, 10, 2)
            },
            OrganizationModel.GetModified(
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
                    Navn = "Changed",
                    Adresse = ["C", "H", "A"],
                    AntallAnsatte = 2,
                    Selskapsform = "Other",
                    StiftelsesDato = new DateOnly(2025, 10, 2)
                }
            )
        );
    }
}
