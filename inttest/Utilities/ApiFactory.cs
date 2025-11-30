using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OrgDemo.Infrastructure;
using OrgDemo.Logic;

namespace OrgDemo.IntTest;

public class ApiFactory<T> : WebApplicationFactory<T> where T : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services
                .RemoveAll<IOrganizationSynchronization>()
                .AddScoped<IOrganizationSynchronization, OrganizationSynchronizationMock>();

            services
                .RemoveAll<IBrregApiService>()
                .AddScoped<IBrregApiService, BrregApiServiceMock>();
            
            services
                .RemoveAll<DatabaseContext>()
                .AddSingleton<DbConnection>(container =>
                {
                    // Do all testing in-memory
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();

                    return connection;
                })
                .AddDbContext<DatabaseContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });
        });

        builder.UseEnvironment("Development");
    }
}