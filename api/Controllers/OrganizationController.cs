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
        return Ok(OrganizationService.Get(organisasjonsNummer));
    }

    [HttpPost("{organisasjonsNummer}")]
    public ActionResult<OrganizationModel> Update(string organisasjonsNummer, OrganizationModel model)
    {
        return Ok(OrganizationService.Update(organisasjonsNummer, model));
    }

    [HttpPost("create")]
    public ActionResult<OrganizationModel> Create(OrganizationModel model)
    {
        return Ok(OrganizationService.Create(model));
    }

    [HttpPost("{organisasjonsNummer}/synchronize")]
    public ActionResult Synchronize(string organisasjonsNummer)
    {
        OrganizationService.Synchronize(organisasjonsNummer);
        return Ok();
    }

    [HttpDelete("{organisasjonsNummer}")]
    public ActionResult Delete(string organisasjonsNummer)
    {
        OrganizationService.Delete(organisasjonsNummer);
        return Ok();
    }
}
