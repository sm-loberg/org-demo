using Microsoft.EntityFrameworkCore;

namespace OrgDemo.System;

public class DatabaseContext : DbContext
{
    public DbSet<Organization> Organizations { get; set; }

    public DatabaseContext()
    {
    }
}
