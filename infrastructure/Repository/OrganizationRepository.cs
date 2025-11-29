using Microsoft.EntityFrameworkCore;
using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly DatabaseContext DatabaseContext;

    public OrganizationRepository(DatabaseContext databaseContext)
    {
        DatabaseContext = databaseContext;
    }

    private async Task<Organization?> Find(OrganizationNumber organisasjonsNummer)
    {
        return await DatabaseContext.Organizations.FindAsync(organisasjonsNummer.Value);
    }

    private async Task SaveChanges()
    {
        await DatabaseContext.SaveChangesAsync();
    }

    public async Task Create(Organization organization)
    {
        DatabaseContext.Add(organization);
        await SaveChanges();
    }

    public async Task<Organization?> Get(OrganizationNumber organisasjonsNummer)
    {
        return await Find(organisasjonsNummer);
    }

    public async Task Update(Organization organization)
    {
        DatabaseContext.Update(organization);
        await SaveChanges();
    }

    public async Task Delete(Organization organization)
    {
        DatabaseContext.Remove(organization);
        await SaveChanges();
    }

    public async Task<List<Organization>> ListAll()
    {
        return await DatabaseContext.Organizations.ToListAsync();
    }

    public async Task UpdateAll(List<Organization> organizations)
    {
        DatabaseContext.Organizations.UpdateRange(organizations);
        await SaveChanges();
    }
}