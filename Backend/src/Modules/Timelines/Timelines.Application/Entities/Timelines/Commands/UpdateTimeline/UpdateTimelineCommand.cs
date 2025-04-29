using BuildingBlocks.Domain.Timelines.Timeline.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Application.Entities.Timelines.Commands.UpdateTimeline;

// ReSharper disable once ClassNeverInstantiated.Global
public record UpdateTimelineCommand : ICommand<UpdateTimelineResult>
{
    public required TimelineId Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}

// ReSharper disable once NotAccessedPositionalProperty.Global
public record UpdateTimelineResult(TimelineBaseDto Timeline);

public class UpdateTimelineCommandValidator : AbstractValidator<UpdateTimelineCommand>
{
    // ReSharper disable once EmptyConstructor
    public UpdateTimelineCommandValidator() { }
}
