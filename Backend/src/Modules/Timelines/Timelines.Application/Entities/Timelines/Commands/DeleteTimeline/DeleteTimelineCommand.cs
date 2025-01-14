namespace Timelines.Application.Entities.Timelines.Commands.DeleteTimeline;

public record DeleteTimelineCommand(string Id) : ICommand<DeleteTimelineResult>;

public record DeleteTimelineResult(bool TimelineDeleted);

public class DeleteTimelineCommandValidator : AbstractValidator<DeleteTimelineCommand>
{
    public DeleteTimelineCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
