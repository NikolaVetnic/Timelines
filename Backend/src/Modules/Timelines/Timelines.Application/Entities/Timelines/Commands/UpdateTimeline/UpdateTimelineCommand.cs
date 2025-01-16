namespace Timelines.Application.Entities.Timelines.Commands.UpdateTimeline;

public record UpdateTimelineCommand(TimelineDto Timeline) : ICommand<UpdateTimelineResult>;

public record UpdateTimelineResult(bool TimelineUpdated);

public class UpdateTimelineCommandValidator : AbstractValidator<UpdateTimelineCommand>
{
    public UpdateTimelineCommandValidator()
    {
        RuleFor(x => x.Timeline.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Timeline.Title).NotEmpty().WithMessage("Title is required.");

        // ToDo: Add remaining Timeline command validators
    }
}
