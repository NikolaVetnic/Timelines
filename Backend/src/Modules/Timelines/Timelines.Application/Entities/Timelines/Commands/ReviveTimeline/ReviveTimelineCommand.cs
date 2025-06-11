using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Timelines.Application.Entities.Timelines.Commands.DeleteTimeline;

namespace Timelines.Application.Entities.Timelines.Commands.ReviveTimeline;

public record ReviveTimelineCommand(TimelineId Id) : ICommand<ReviveTimelineResponse>
{
    public ReviveTimelineCommand(string Id) : this(TimelineId.Of(Guid.Parse(Id))) { }
}

public record ReviveTimelineResponse(bool TimelineRevived);

public class ReviveTimelineCommandValidator : AbstractValidator<ReviveTimelineCommand>
{
    public ReviveTimelineCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
