using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using Nodes.Application.Entities.Nodes.Queries.ListRemindersByNodeId;

namespace Nodes.Api.Endpoints.Nodes;

public class ListRemindersByNodeId : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Nodes/{nodeId}/Reminders", async (string nodeId, [AsParameters] PaginationRequest query, ISender sender) =>
            {
                var result = await sender.Send(new ListRemindersByNodeIdQuery(nodeId, query));
                var response = result.Adapt<ListRemindersByNodeIdResponse>();
                return Results.Ok(response);
            })
            .WithName("ListRemindersByNodeId")
            .Produces<ListRemindersByNodeIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("List Reminder by Node Id")
            .WithDescription("List Reminder by Node Id");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record ListRemindersByNodeIdResponse(PaginatedResult<ReminderBaseDto> Reminders);
