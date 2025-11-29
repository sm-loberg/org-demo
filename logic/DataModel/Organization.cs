using System.Text.Json;

namespace OrgDemo.Logic;

public class Organization
{
    public string OrganisasjonsNummer { get; private set; } = "";
    public string Navn { get; private set; } = "";
    public List<string> Adresse { get; private set; } = [];
    public int AntallAnsatte { get; private set; } = 0;
    public string Selskapsform { get; private set; } = "";
    public DateOnly StiftelsesDato { get; private set; } = DateOnly.MinValue;

    public string SourceJson { get; private set; } = "";
    public DateTime CreatedAt { get; private set; } = DateTime.MinValue;
    public DateTime UpdatedAt { get; private set; } = DateTime.MinValue;

    public Organization()
    {
    }

    public Organization(OrganizationNumber organisasjonsNummer, OrganizationModel sourceModel)
    {
        OrganisasjonsNummer = organisasjonsNummer.Value;
        SetSource(sourceModel);
        
        CreatedAt = UpdatedAt = DateTime.UtcNow;
    }

    private void MarkUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateFromModel(OrganizationModel model)
    {
        Navn = model.Navn ?? Navn;
        Adresse = model.Adresse ?? Adresse;
        AntallAnsatte = model.AntallAnsatte ?? AntallAnsatte;
        Selskapsform = model.Selskapsform ?? Selskapsform;
        StiftelsesDato = model.StiftelsesDato ?? StiftelsesDato;

        MarkUpdated();
    }

    public void SetSource(OrganizationModel model)
    {
        SourceJson = JsonSerializer.Serialize(model);
    }

    public OrganizationModel GetSourceModel()
    {
        return JsonSerializer.Deserialize<OrganizationModel>(SourceJson) ?? new OrganizationModel();
    }
}