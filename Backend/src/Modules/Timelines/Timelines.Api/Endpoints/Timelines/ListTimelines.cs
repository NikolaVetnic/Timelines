using BuildingBlocks.Application.Pagination;
using Timelines.Application.Entities.Timelines.Queries.ListTimelines;

namespace Timelines.Api.Endpoints.Timelines;

public class ListTimelines : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Timelines", async ([AsParameters] PaginationRequest query, ISender sender) =>
        {
            var result = await sender.Send(new ListTimelinesQuery(query));
            var response = result.Adapt<ListTimelinesResponse>();

            return Results.Ok(response);
        })
        .WithName("ListTimelines")
        .Produces<ListTimelinesResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("List Timelines")
        .WithDescription("List Timelines");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record ListTimelinesResponse(PaginatedResult<TimelineDto> Timelines);
