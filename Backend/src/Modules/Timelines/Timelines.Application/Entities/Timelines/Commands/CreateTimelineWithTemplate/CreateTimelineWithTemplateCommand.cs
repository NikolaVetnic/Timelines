using BuildingBlocks.Domain.Timelines.Timeline.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

// ReSharper disable NotAccessedPositionalProperty.Global

namespace Timelines.Application.Entities.Timelines.Commands.CreateTimelineWithTemplate;

public record CreateTimelineWithTemplateCommand : ICommand<CreateTimelineWithTemplateResult>
{
    public required TimelineId Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}

// ReSharper disable once ClassNeverInstantiated.Global
public record CreateTimelineWithTemplateResult(TimelineBaseDto Timeline);
