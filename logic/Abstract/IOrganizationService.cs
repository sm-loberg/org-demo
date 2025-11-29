namespace OrgDemo.Logic;

public interface IOrganizationService
{
    public OrganizationModel Create(string organisasjonsNummer, OrganizationModel model);
    public OrganizationModel Get(string organisasjonsNummer);
    public OrganizationModel Update(string organisasjonsNummer, OrganizationModel model);
    public void Delete(string organisasjonsNummer);

    public OrganizationModel Synchronize(string organisasjonsNummer);
}
