using OrgDemo.Logic;

public class OrganizationSynchronizationMock : IOrganizationSynchronization
{
    public Task SynchronizeAll()
    {
        return Task.CompletedTask;
    }
}
