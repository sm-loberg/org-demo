using Microsoft.EntityFrameworkCore;
using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public class DatabaseContext : DbContext
{
    public DbSet<Organization> Organizations { get; set; }

    public DatabaseContext()
    {
    }
}
