using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Timelines.Application.Entities.Phases.Commands.CreatePhase;
using Timelines.Application.Entities.Phases.Queries.GetPhaseById;

namespace Timelines.Api.Controllers.Phases;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class PhasesController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreatePhaseResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreatePhaseResponse>> Create([FromBody] CreatePhaseRequest request)
    {
        var command = request.Adapt<CreatePhaseCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<CreatePhaseResponse>();

        return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
    }

    [HttpGet("{phaseId}")]
    [ProducesResponseType(typeof(GetPhaseByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPhaseByIdResponse>> GetById(
        [FromRoute] string phaseId)
    {
        var result = await sender.Send(new GetPhaseByIdQuery(phaseId));

        if (result is null)
            return NotFound();

        var response = result.Adapt<GetPhaseByIdResponse>();

        return Ok(response);
    }
}
