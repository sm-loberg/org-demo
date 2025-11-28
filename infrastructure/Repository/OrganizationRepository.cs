using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly DatabaseContext DatabaseContext;

    public OrganizationRepository(DatabaseContext databaseContext)
    {
        DatabaseContext = databaseContext;
    }

    private Organization? Find(string organisasjonsNummer)
    {
        return DatabaseContext.Organizations.Find(organisasjonsNummer);
    }

    private void SaveChanges()
    {
        DatabaseContext.SaveChanges();
    }

    public void Create(Organization organization)
    {
        DatabaseContext.Add(organization);
        SaveChanges();
    }

    public Organization? Get(string organisasjonsNummer)
    {
        return Find(organisasjonsNummer);
    }

    public void Update(Organization organization)
    {
        DatabaseContext.Update(organization);
        SaveChanges();
    }

    public void Delete(Organization organization)
    {
        DatabaseContext.Remove(organization);
        SaveChanges();
    }
}