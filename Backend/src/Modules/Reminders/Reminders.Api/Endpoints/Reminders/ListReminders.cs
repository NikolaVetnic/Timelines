using BuildingBlocks.Application.Pagination;
using Reminders.Application.Entities.Reminders.Queries.ListReminders;

namespace Reminders.Api.Endpoints.Reminders;

public class ListReminders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Reminders", async ([AsParameters] PaginationRequest query, ISender sender) =>
            {
                var result = await sender.Send(new ListRemindersQuery(query));
                var response = result.Adapt<ListRemindersResponse>();

                return Results.Ok(response);
            })
            .WithName("ListReminders")
            .Produces<ListRemindersResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("List Reminders")
            .WithDescription("List Reminders");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record ListRemindersResponse(PaginatedResult<ReminderDto> Reminders);
