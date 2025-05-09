using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Reminders.Application.Entities.Reminders.Commands.CreateReminder;
using System.Threading.Tasks;
using Reminders.Application.Entities.Reminders.Queries.GetReminderById;
using BuildingBlocks.Application.Pagination;
using Reminders.Application.Entities.Reminders.Queries.ListReminders;
using System;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using Reminders.Application.Entities.Reminders.Commands.DeleteReminder;
using Reminders.Application.Entities.Reminders.Commands.UpdateReminder;

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

    [HttpGet]
    [ProducesResponseType(typeof(ListRemindersResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListRemindersResponse>> Get([FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListRemindersQuery(query));
        var response = result.Adapt<ListRemindersResponse>();

        return Ok(response);
    }

    [HttpPut("{reminderId}")]
    [ProducesResponseType(typeof(UpdateReminderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateReminderResponse>> Update([FromRoute] string reminderId, [FromBody] UpdateReminderRequest request)
    {
        var command = new UpdateReminderCommand
        {
            Id = ReminderId.Of(Guid.Parse(reminderId)),
            Title = request.Title,
            Description = request.Description,
            NotifyAt = request.NotifyAt,
            Priority = request.Priority,
            NodeId = request.NodeId,
        };

        var result = await sender.Send(command);
        var response = result.Adapt<UpdateReminderResponse>();

        return Ok(response);
    }

    [HttpDelete("{reminderId}")]
    [ProducesResponseType(typeof(DeleteReminderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeleteReminderResponse>> Delete([FromRoute] string reminderId)
    {
        var result = await sender.Send(new DeleteReminderCommand(reminderId));
        var response = result.Adapt<DeleteReminderResponse>();

        return Ok(response);
    }
}
