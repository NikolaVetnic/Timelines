using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Reminders.Application.Entities.Reminders.Commands.CreateReminder;
using System.Threading.Tasks;

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
}
