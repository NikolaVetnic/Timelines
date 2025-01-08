using Timelines.Application.Entities.Timelines.Commands.UpdateTimeline;

namespace Timelines.Api.Endpoints.Timelines;

public class UpdateTimeline : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Timelines", async (UpdateTimelineRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateTimelineCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateTimelineResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateTimeline")
            .Produces<UpdateTimelineResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Timeline")
            .WithDescription("Update Timeline");
    }
}

public record UpdateTimelineRequest(TimelineDto Timeline);

public record UpdateTimelineResponse(bool TimelineUpdated);
