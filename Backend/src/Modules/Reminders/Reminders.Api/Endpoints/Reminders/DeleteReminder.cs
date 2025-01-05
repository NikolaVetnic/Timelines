using Reminders.Application.Entities.Reminders.Commands.DeleteReminder;

namespace Reminders.Api.Endpoints.Reminders;

public class DeleteReminder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/Reminders/{reminderId}", async (string reminderId, ISender sender) =>
            {
                var result = await sender.Send(new DeleteReminderCommand(reminderId));
                var response = result.Adapt<DeleteReminderResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteReminder")
            .Produces<DeleteReminderResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Reminder")
            .WithDescription("Delete Reminder");
    }
}

public record DeleteReminderResponse(bool ReminderDeleted);
