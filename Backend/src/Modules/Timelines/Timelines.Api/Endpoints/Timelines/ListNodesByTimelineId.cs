using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using Timelines.Application.Entities.Timelines.Queries.ListNodesByTimelineId;

namespace Timelines.Api.Endpoints.Timelines;

public class ListNodesByTimelineId : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Timelines/{timelineId}/Nodes", async (string timelineId, [AsParameters] PaginationRequest query, ISender sender) =>
        {
                var result = await sender.Send(new ListNodesByTimelineIdQuery(timelineId, query));
                var response = result.Adapt<ListNodesByTimelineIdResponse>();
                return Results.Ok(response);
            })
            .WithName("ListNodesByTimelineId")
            .Produces<ListNodesByTimelineIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("List Nodes by Timeline Id")
            .WithDescription("List Nodes by Timeline Id");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record ListNodesByTimelineIdResponse(PaginatedResult<NodeBaseDto> Nodes);
