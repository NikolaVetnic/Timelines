using Timelines.Application.Entities.Timelines.Commands.CreateTimeline;

namespace Timelines.Application.Timelines.Commands.CreateTimeline;

internal class CreateTimelineHandler(ITimelinesDbContext dbContext) :
    ICommandHandler<CreateTimelineCommand, CreateTimelineResult>
{
    public async Task<CreateTimelineResult> Handle(CreateTimelineCommand command, CancellationToken cancellationToken)
    {
        var timeline = Timeline.Create(
            TimelineId.Of(Guid.NewGuid()),
            command.Timeline.Title,
            command.Timeline.Description
        );

        dbContext.Timelines.Add(timeline);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateTimelineResult(timeline.Id);
    }
}
