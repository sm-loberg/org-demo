using OrgDemo.Logic;
using OrgDemo.Infrastructure;
using OrgDemo.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IBrregApiService, BrregApiService>();
builder.Services.AddScoped<IOrganizationSynchronization, OrganizationSynchronization>();
builder.Services.AddHostedService<SynchronizationBackgroundService>();
builder.Services.AddDbContext<DatabaseContext>(opt => opt.UseSqlite("Filename=data.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // For quick and simple testing
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();