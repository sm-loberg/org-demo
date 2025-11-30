using OrgDemo.Logic;

namespace OrgDemo.Api;

public class SynchronizationBackgroundService : BackgroundService
{
    private readonly TimeSpan SynchronizationInterval;
    private readonly IServiceProvider ServiceProvider;

    public SynchronizationBackgroundService(IConfiguration configuration, IServiceProvider serviceProvider)
        : base()
    {
        SynchronizationInterval = TimeSpan.FromSeconds(configuration.GetValue<int>("SynchronizationInterval"));
        ServiceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        PeriodicTimer timer = new(SynchronizationInterval);

        // Always fire immediately on startup, instead of waiting the initial interval
        do
        {
            using(IServiceScope scope = ServiceProvider.CreateScope())
            {
                var organizationSynchronization = scope.ServiceProvider.GetRequiredService<IOrganizationSynchronization>();
                
                try
                {
                    await organizationSynchronization.SynchronizeAll();
                }
                catch(Exception)
                {
                    // Logging or other handling..
                }
            }
        }
        while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}
