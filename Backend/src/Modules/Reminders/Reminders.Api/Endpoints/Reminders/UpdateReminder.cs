using Reminders.Application.Entities.Reminders.Commands.UpdateReminder;

namespace Reminders.Api.Endpoints.Reminders;

public class UpdateReminder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Reminders", async (UpdateReminderRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateReminderCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateReminderResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateReminder")
            .Produces<UpdateReminderResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Reminder")
            .WithDescription("Update Reminder");
    }
}

public record UpdateReminderRequest(ReminderDto Reminder);

public record UpdateReminderResponse(bool IsSuccess);
