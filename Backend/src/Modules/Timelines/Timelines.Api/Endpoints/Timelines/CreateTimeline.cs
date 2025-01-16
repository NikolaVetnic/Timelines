using BuildingBlocks.Domain.ValueObjects.Ids;
using Timelines.Application.Entities.Timelines.Commands.CreateTimeline;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Timelines.Api.Endpoints.Timelines;

public class CreateTimeline : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Timelines", async (CreateTimelineRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateTimelineCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateTimelineResponse>();

            return Results.Created($"/Timelines/{response.Id}", response);
        })
        .WithName("CreateTimeline")
        .Produces<CreateTimelineResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Timeline")
        .WithDescription("Creates a new timeline");
    }
}

public record CreateTimelineRequest
{
    public CreateTimelineRequest() { }

    public CreateTimelineRequest(TimelineDto timeline) => Timeline = timeline;

    public TimelineDto Timeline { get; set; }
}

public record CreateTimelineResponse(TimelineId Id);
