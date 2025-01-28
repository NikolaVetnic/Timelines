using System;
using BuildingBlocks.Domain.Reminders.ValueObjects;
using BuildingBlocks.Domain.ValueObjects.Ids;
using Reminders.Application.Entities.Reminders.Commands.CreateReminder;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Reminders.Api.Endpoints.Reminders;

public class CreateReminder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Reminders", async (CreateReminderRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateReminderCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateReminderResponse>();

            return Results.Created($"/Reminders/{response.Id}", response);
        })
        .WithName("CreateReminder")
        .Produces<CreateReminderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Reminder")
        .WithDescription("Creates a new reminder");
    }
}

public record CreateReminderRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDateTime { get; set; }
    public int Priority { get; set; } // todo: This should be an enum common for all Priority properties
    public DateTime NotificationTime { get; set; }
    public string Status { get; set; }
    public NodeId NodeId { get; set; }
}

public record CreateReminderResponse(ReminderId Id);
