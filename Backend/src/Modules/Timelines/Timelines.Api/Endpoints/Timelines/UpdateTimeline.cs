using System;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Timelines.Application.Entities.Timelines.Commands.UpdateTimeline;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Timelines.Api.Endpoints.Timelines;

public class UpdateTimeline : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Timelines/{timelineId}", async (string timelineId, UpdateTimelineRequest request, ISender sender) =>
            {
                var command = new UpdateTimelineCommand
                {
                    Id = TimelineId.Of(Guid.Parse(timelineId)),
                    Title = request.Title,
                    Description = request.Description
                };
                
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateTimelineResult>();
                
                return Results.Ok(response);
            })
        .WithName("UpdateTimeline")
        .Produces<UpdateTimelineResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Timeline")
        .WithDescription("Updates a timeline");
    }
}

public class UpdateTimelineRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public record UpdateTimelineResponse(TimelineBaseDto Timeline);
