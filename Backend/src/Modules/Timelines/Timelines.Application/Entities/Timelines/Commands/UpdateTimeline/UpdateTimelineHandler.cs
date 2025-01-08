using Timelines.Application.Entities.Timelines.Exceptions;

namespace Timelines.Application.Entities.Timelines.Commands.UpdateTimeline;

public class UpdateTimelineHandler(ITimelinesDbContext dbContext) : ICommandHandler<UpdateTimelineCommand, UpdateTimelineResult>
{
    public async Task<UpdateTimelineResult> Handle(UpdateTimelineCommand command, CancellationToken cancellationToken)
    {
        var timeline = await dbContext.Timelines
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == TimelineId.Of(Guid.Parse(command.Timeline.Id)), cancellationToken);

        if (timeline is null)
            throw new TimelineNotFoundException(command.Timeline.Id);

        UpdateTimelineWithNewValues(timeline, command.Timeline);

        dbContext.Timelines.Update(timeline);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateTimelineResult(true);
    }

    private static void UpdateTimelineWithNewValues(Timeline timeline, TimelineDto timelineDto)
    {
        timeline.Update(
            timelineDto.Title);
    }
}
