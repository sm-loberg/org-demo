using Microsoft.EntityFrameworkCore;
using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public class DatabaseContext : DbContext
{
    public DbSet<Organization> Organizations { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Organization>()
            .HasKey(p => p.OrganisasjonsNummer);
    }
}
