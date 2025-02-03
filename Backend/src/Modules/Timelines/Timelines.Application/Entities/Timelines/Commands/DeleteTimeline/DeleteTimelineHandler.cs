using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Timelines.Exceptions;

namespace Timelines.Application.Entities.Timelines.Commands.DeleteTimeline;

public class DeleteTimelineHandler(ITimelinesDbContext dbContext) : ICommandHandler<DeleteTimelineCommand, DeleteTimelineResult>
{
    public async Task<DeleteTimelineResult> Handle(DeleteTimelineCommand command, CancellationToken cancellationToken)
    {
        var timeline = await dbContext.Timelines
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == command.Id, cancellationToken);

        if (timeline is null)
            throw new TimelineNotFoundException(command.Id.ToString());

        dbContext.Timelines.Remove(timeline);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteTimelineResult(true);
    }
}
