using BuildingBlocks.Domain.ValueObjects.Ids;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Timelines.Application.Dtos;
using Timelines.Application.Timelines.Commands.CreateTimeline;

namespace Timelines.Api.Endpoints;

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

public record CreateTimelineRequest(TimelineDto Timeline);

public record CreateTimelineResponse(TimelineId Id);
