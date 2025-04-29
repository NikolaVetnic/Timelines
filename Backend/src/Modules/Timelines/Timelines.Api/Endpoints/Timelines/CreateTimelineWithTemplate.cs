using System;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Timelines.Application.Entities.Timelines.Commands.CreateTimelineWithTemplate;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Timelines.Api.Endpoints.Timelines;

public class CreateTimelineWithTemplate : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Timelines/Clone/{timelineId}", async (string timelineId, CreateTimelineWithTemplateRequest request, ISender sender) =>
            {
                var command = new CreateTimelineWithTemplateCommand
                {
                    Id = TimelineId.Of(Guid.Parse(timelineId)),
                    Title = request.Title,
                    Description = request.Description
                };

            var result = await sender.Send(command);
            var response = result.Adapt<CreateTimelineWithTemplateResponse>();

            return Results.Created($"/Timelines/Clone/{response.Timeline.Id}", response);
        })
        .WithName("CreateTimelineWithTemplate")
        .Produces<CreateTimelineWithTemplateResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Timeline with Template")
        .WithDescription("Creates a new timeline using a template");
    }
}

public class CreateTimelineWithTemplateRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public record CreateTimelineWithTemplateResponse(TimelineBaseDto Timeline);
