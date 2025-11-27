using OrgDemo.Logic;

namespace OrgDemo.Infrastructure;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly DatabaseContext DatabaseContext;

    public OrganizationRepository(DatabaseContext databaseContext)
    {
        DatabaseContext = databaseContext;
    }

    public Organization Create(string organisasjonsNummer)
    {
        return DatabaseContext.Add(new Organization(organisasjonsNummer)).Entity;
    }

    public void Delete(Organization organization)
    {
        DatabaseContext.Remove(organization);
    }

    public Organization? Get(string organisasjonsNummer)
    {
        return DatabaseContext.Organizations.Find(organisasjonsNummer);
    }
}