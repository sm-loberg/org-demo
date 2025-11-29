using Microsoft.AspNetCore.Mvc;
using OrgDemo.Logic;

namespace OrgDemo.Api;

[Route("api/v1/organization")]
[ApiController]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService OrganizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        OrganizationService = organizationService;
    }

    [HttpGet("{organisasjonsNummer}")]
    public ActionResult<OrganizationModel> Get(string organisasjonsNummer)
    {
        return Ok(OrganizationService.Get(OrganizationNumber.FromString(organisasjonsNummer)));
    }

    [HttpPost("{organisasjonsNummer}")]
    public ActionResult<OrganizationModel> Update(string organisasjonsNummer, OrganizationModel model)
    {
        return Ok(OrganizationService.Update(OrganizationNumber.FromString(organisasjonsNummer), model));
    }

    [HttpPost("{organisasjonsNummer}/create")]
    public ActionResult<OrganizationModel> Create(string organisasjonsNummer, OrganizationModel model)
    {
        return Ok(OrganizationService.Create(OrganizationNumber.FromString(organisasjonsNummer), model));
    }

    [HttpGet("{organisasjonsNummer}/synchronize")]
    public ActionResult<OrganizationModel> Synchronize(string organisasjonsNummer)
    {
        return Ok(OrganizationService.Synchronize(OrganizationNumber.FromString(organisasjonsNummer)));
    }

    [HttpDelete("{organisasjonsNummer}")]
    public ActionResult Delete(string organisasjonsNummer)
    {
        OrganizationService.Delete(OrganizationNumber.FromString(organisasjonsNummer));
        return Ok();
    }
}
