using Timelines.Application.Entities.Timelines.Dtos;

namespace Timelines.Application.Entities.Timelines.Commands.CreateTimeline;

// ReSharper disable once ClassNeverInstantiated.Global
public record CreateTimelineCommand(TimelineDto Timeline) : ICommand<CreateTimelineResult>;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record CreateTimelineResult(TimelineId Id);

public class CreateTimelineCommandValidator : AbstractValidator<CreateTimelineCommand>
{
    public CreateTimelineCommandValidator()
    {
        RuleFor(x => x.Timeline.Title).NotEmpty().WithMessage("Title is required.");

        // ToDo: Add remaining Timeline command validators
    }
}
