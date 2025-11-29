namespace OrgDemo.Logic;

public interface IOrganizationService
{
    public OrganizationModel Create(OrganizationNumber organisasjonsNummer, OrganizationModel model);
    public OrganizationModel Get(OrganizationNumber organisasjonsNummer);
    public OrganizationModel Update(OrganizationNumber organisasjonsNummer, OrganizationModel model);
    public void Delete(OrganizationNumber organisasjonsNummer);

    public OrganizationModel Synchronize(OrganizationNumber organisasjonsNummer);
}
