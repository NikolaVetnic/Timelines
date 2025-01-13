using Timelines.Application.Entities.Timelines.Exceptions;

namespace Timelines.Application.Entities.Timelines.Commands.DeleteTimeline;

public class DeleteNodeHandler(ITimelinesDbContext dbContext) : ICommandHandler<DeleteTimelineCommand, DeleteTimelineResult>
{
    public async Task<DeleteTimelineResult> Handle(DeleteTimelineCommand command, CancellationToken cancellationToken)
    {
        var timeline = await dbContext.Timelines
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == TimelineId.Of(Guid.Parse(command.TimelineId)), cancellationToken);

        if (timeline is null)
            throw new TimelineNotFoundException(command.TimelineId);

        dbContext.Timelines.Remove(timeline);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteTimelineResult(true);
    }
}
