using EyeC.Application.Offices.Commands.CreateOffice;
using EyeC.Application.Offices.Commands.DeleteOffice;
using EyeC.Application.Offices.Commands.UpdateOffice;
using EyeC.Application.Offices.Queries.GetOffices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EyeC.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OfficesController : ControllerBase
{
    private readonly IMediator _mediator;

    public OfficesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetOffices()
    {
        var result = await _mediator.Send(new GetOfficesQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOffice(CreateOfficeCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateOffice(UpdateOfficeCommandHandler command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{officeId}")]
    public async Task<IActionResult> DeleteOffice([FromRoute] int officeId)
    {
        var result = await _mediator.Send(new DeleteOfficeCommand(officeId));
        return Ok(result);
    }
}
