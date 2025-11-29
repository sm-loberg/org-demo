using OrgDemo.Logic;

namespace OrgDemo.UnitTest;

public class TestOrganizationNumber
{
    [Fact]
    public void TestFromString()
    {
        Assert.Equal(
            OrgDemoException.ErrorCode.InvalidOrganizationNumberFormat,
            Assert.Throws<OrgDemoException>(() => OrganizationNumber.FromString("")).Code
        );

        Assert.Equal(
            OrgDemoException.ErrorCode.InvalidOrganizationNumberFormat,
            Assert.Throws<OrgDemoException>(() => OrganizationNumber.FromString("599999999")).Code
        );

        Assert.Equal(
            OrgDemoException.ErrorCode.InvalidOrganizationNumberFormat,
            Assert.Throws<OrgDemoException>(() => OrganizationNumber.FromString("9123")).Code
        );

        Assert.Equal(
            OrgDemoException.ErrorCode.InvalidOrganizationNumberFormat,
            Assert.Throws<OrgDemoException>(() => OrganizationNumber.FromString("8123")).Code
        );

        Assert.Equal(
            OrgDemoException.ErrorCode.InvalidOrganizationNumberFormat,
            Assert.Throws<OrgDemoException>(() => OrganizationNumber.FromString("9999999999")).Code
        );

        Assert.Equal(
            OrgDemoException.ErrorCode.InvalidOrganizationNumberFormat,
            Assert.Throws<OrgDemoException>(() => OrganizationNumber.FromString("-999999999")).Code
        );

        Assert.Equal("999999999", OrganizationNumber.FromString("999999999").Value);
        Assert.Equal("823123123", OrganizationNumber.FromString("823123123").Value);
        Assert.Equal("999999999", OrganizationNumber.FromString("999999999").Value);
        Assert.Equal("987987987", OrganizationNumber.FromString("987987987").Value);
    }
}
