namespace Timelines.Application.Entities.Timelines.Commands.DeleteTimeline;

public record DeleteTimelineCommand(string TimelineId) : ICommand<DeleteTimelineResult>;

public record DeleteTimelineResult(bool TimelineDeleted);

public class DeleteTimelineCommandValidator : AbstractValidator<DeleteTimelineCommand>
{
    public DeleteTimelineCommandValidator()
    {
        RuleFor(x => x.TimelineId).NotEmpty().WithMessage("TimelineId is required");
    }
}
