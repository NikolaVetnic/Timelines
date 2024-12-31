using System.Text.Json.Serialization;
using Timelines.Application.Entities.Timelines.Queries.GetTimelineById;

namespace Timelines.Api.Endpoints.Timelines;

// ReSharper disable once UnusedType.Global
public class GetTimelineById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Timelines/{timelineId}", async (string timelineId, ISender sender) =>
            {
                var result = await sender.Send(new GetTimelineByIdQuery(timelineId));
                var response = result.Adapt<GetTimelineByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetTimelineById")
            .Produces<GetTimelineByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Timeline by Id")
            .WithDescription("Get Timeline by Id");
    }
}


// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record GetTimelineByIdResponse([property: JsonPropertyName("timeline")] TimelineDto TimelineDto);
