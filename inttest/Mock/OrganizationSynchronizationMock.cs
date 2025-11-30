using OrgDemo.Infrastructure;

namespace OrgDemo.IntTest;

public class OrganizationSynchronizationMock : IOrganizationSynchronization
{
    public Task SynchronizeAll()
    {
        return Task.CompletedTask;
    }
}
