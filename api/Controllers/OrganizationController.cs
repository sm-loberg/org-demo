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

    [HttpGet("{id}")]
    public async Task<ActionResult<OrganizationModel>> Get(long id)
    {
        return Ok(OrganizationService.Get(id));
    }

    [HttpPost("{id}")]
    public async Task<ActionResult<OrganizationModel>> Update(long id, OrganizationModel model)
    {
        return Ok(OrganizationService.Update(id, model));
    }

    [HttpPost("create")]
    public async Task<ActionResult<OrganizationModel>> Create(OrganizationModel model)
    {
        return Ok(OrganizationService.Create(model));
    }

    [HttpPost("{id}/synchronize")]
    public ActionResult Synchronize(long id)
    {
        OrganizationService.Synchronize(id);
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(long id)
    {
        OrganizationService.Delete(id);
        return Ok();
    }
}
