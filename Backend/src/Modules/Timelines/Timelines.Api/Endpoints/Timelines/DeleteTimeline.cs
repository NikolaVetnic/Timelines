using Timelines.Application.Entities.Timelines.Commands.DeleteTimeline;

namespace Timelines.Api.Endpoints.Timelines;

public class DeleteTimeline : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/Timelines/{timelineId}", async (string timelineId, ISender sender) =>
            {
                var result = await sender.Send(new DeleteTimelineCommand(timelineId));
                var response = result.Adapt<DeleteTimelineResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteTimeline")
            .Produces<DeleteTimelineResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Timeline")
            .WithDescription("Delete Timeline");
    }
}

public record DeleteTimelineResponse(bool TimelineDeleted);
