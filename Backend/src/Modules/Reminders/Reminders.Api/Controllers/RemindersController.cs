using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Reminders.Application.Entities.Reminders.Commands.CreateReminder;
using System.Threading.Tasks;
using Reminders.Application.Entities.Reminders.Queries.GetReminderById;

namespace Reminders.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class RemindersController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateReminderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateReminderResponse>> Create([FromBody] CreateReminderRequest request)
    {
        var command = request.Adapt<CreateReminderCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<CreateReminderResponse>();

        return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
    }

    [HttpGet("{reminderId}")]
    [ProducesResponseType(typeof(GetReminderByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetReminderByIdResponse>> GetById([FromRoute] string reminderId)
    {
        var result = await sender.Send(new GetReminderByIdQuery(reminderId));

        if (result is null)
            return NotFound();

        var response = result.Adapt<GetReminderByIdResponse>();

        return Ok(response);
    }
}
