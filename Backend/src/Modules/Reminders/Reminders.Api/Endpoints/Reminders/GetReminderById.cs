using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using Reminders.Application.Entities.Reminders.Queries.GetReminderById;

namespace Reminders.Api.Endpoints.Reminders;

// ReSharper disable once UnusedType.Global
public class GetReminderById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Reminders/{reminderId}", async (string reminderId, ISender sender) =>
        {
            var result = await sender.Send(new GetReminderByIdQuery(reminderId));
            var response = result.Adapt<GetReminderByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetReminderById")
        .Produces<GetReminderByIdResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Reminder by Id")
        .WithDescription("Get Reminder by Id");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record GetReminderByIdResponse([property: JsonPropertyName("reminder")] ReminderDto ReminderDto);
