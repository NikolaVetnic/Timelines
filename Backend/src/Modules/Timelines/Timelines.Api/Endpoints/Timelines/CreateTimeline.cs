using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
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

public class CreateTimelineRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public record CreateTimelineResponse(TimelineId Id);
