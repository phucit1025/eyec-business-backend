using EyeC.Application.Doctors.Command.CreateDoctor;
using EyeC.Application.Doctors.Command.DeleteDoctor;
using EyeC.Application.Doctors.Command.UpdateDoctor;
using EyeC.Application.Doctors.Queries.GetAllDoctors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EyeC.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DoctorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDoctors()
    {
        var result = await _mediator.Send(new GetAllDoctorsQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor(CreateDoctorCommand command)
    {
        command.FeatureImageByte = Convert.FromBase64String(command.Base64!);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateDoctor(UpdateDoctorCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{doctorId}")]
    public async Task<IActionResult> DeleteDoctor([FromRoute] int doctorId)
    {
        var result = await _mediator.Send(new DeleteDoctorCommand(doctorId));
        return Ok(result);
    }
}
