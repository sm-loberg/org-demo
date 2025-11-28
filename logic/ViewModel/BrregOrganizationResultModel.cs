namespace OrgDemo.Logic;

public class BrregOrganizationResultModel
{
    public required string SourceJson { get; set; }

    public BrregOrganizationResultModel(string sourceJson)
    {
        SourceJson = sourceJson;
    }

    public OrganizationModel ToModel()
    {
        throw new NotImplementedException();
    }
}
