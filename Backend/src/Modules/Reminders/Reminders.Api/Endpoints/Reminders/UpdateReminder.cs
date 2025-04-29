using System;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using Reminders.Application.Entities.Reminders.Commands.UpdateReminder;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Reminders.Api.Endpoints.Reminders;

public class UpdateReminder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Reminders/{reminderId}", async (string reminderId, UpdateReminderRequest request, ISender sender) =>
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
                
                return Results.Ok(response);
            })
        .WithName("UpdateReminder")
        .Produces<UpdateReminderResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Reminder")
        .WithDescription("Updates a reminder");
    }
}

public record UpdateReminderRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime NotifyAt { get; set; }
    public int Priority { get; set; }
    public NodeId NodeId { get; set; }
}

public record UpdateReminderResponse(ReminderDto Reminder);
