using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Application.Entities.Timelines.Commands.CreateTimeline;

// ReSharper disable once ClassNeverInstantiated.Global
public record CreateTimelineCommand : ICommand<CreateTimelineResult>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
}

// ReSharper disable once NotAccessedPositionalProperty.Global
public record CreateTimelineResult(TimelineId Id);

public class CreateTimelineCommandValidator : AbstractValidator<CreateTimelineCommand>
{
    public CreateTimelineCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");

        // ToDo: Add remaining Timeline command validators
    }
}
