using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly DatabaseContext DatabaseContext;

    public OrganizationRepository(DatabaseContext databaseContext)
    {
        DatabaseContext = databaseContext;
    }

    private Organization? Find(OrganizationNumber organisasjonsNummer)
    {
        return DatabaseContext.Organizations.Find(organisasjonsNummer.Value);
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

    public Organization? Get(OrganizationNumber organisasjonsNummer)
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