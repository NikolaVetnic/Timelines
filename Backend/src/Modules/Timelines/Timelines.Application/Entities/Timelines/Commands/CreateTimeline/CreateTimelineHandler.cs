using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Timelines.Application.Data.Abstractions;

namespace Timelines.Application.Entities.Timelines.Commands.CreateTimeline;

internal class CreateTimelineHandler(ICurrentUser currentUser, ITimelinesRepository timelinesRepository)
    : ICommandHandler<CreateTimelineCommand, CreateTimelineResult>
{
    public async Task<CreateTimelineResult> Handle(CreateTimelineCommand command, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId!;
        var timeline = command.ToTimeline(userId);

        await timelinesRepository.CreateTimelineAsync(timeline, cancellationToken);

        return new CreateTimelineResult(timeline.Id);
    }
}

internal static class CreateTimelineCommandExtensions
{
    public static Timeline ToTimeline(this CreateTimelineCommand command, string userId)
    {
        return Timeline.Create(
            TimelineId.Of(Guid.NewGuid()),
            command.Title,
            command.Description,
            userId
        );
    }
}
