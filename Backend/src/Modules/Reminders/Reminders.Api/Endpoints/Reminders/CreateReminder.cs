using BuildingBlocks.Domain.ValueObjects.Ids;
using Reminders.Application.Entities.Reminders.Commands.CreateReminder;

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

public record CreateReminderRequest(ReminderDto Reminder);

public record CreateReminderResponse(ReminderId Id);
