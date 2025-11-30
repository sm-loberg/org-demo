using Microsoft.AspNetCore.Mvc;
using OrgDemo.Infrastructure;
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
    public async Task<ActionResult<OrganizationModel>> Get(string organisasjonsNummer)
    {
        return Ok(await OrganizationService.Get(OrganizationNumber.FromString(organisasjonsNummer)));
    }

    [HttpPost("{organisasjonsNummer}")]
    public async Task<ActionResult<OrganizationModel>> Update(string organisasjonsNummer, OrganizationModel model)
    {
        return Ok(await OrganizationService.Update(OrganizationNumber.FromString(organisasjonsNummer), model));
    }

    [HttpPost("{organisasjonsNummer}/create")]
    public async Task<ActionResult<OrganizationModel>> Create(string organisasjonsNummer, OrganizationModel model)
    {
        return Ok(await OrganizationService.Create(OrganizationNumber.FromString(organisasjonsNummer), model));
    }

    [HttpGet("{organisasjonsNummer}/synchronize")]
    public async Task<ActionResult<OrganizationModel>> Synchronize(string organisasjonsNummer)
    {
        return Ok(await OrganizationService.Synchronize(OrganizationNumber.FromString(organisasjonsNummer)));
    }

    [HttpDelete("{organisasjonsNummer}")]
    public async Task<ActionResult> Delete(string organisasjonsNummer)
    {
        await OrganizationService.Delete(OrganizationNumber.FromString(organisasjonsNummer));
        return Ok();
    }
}
