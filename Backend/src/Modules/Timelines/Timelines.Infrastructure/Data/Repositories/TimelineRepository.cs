using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Timelines.Exceptions;

namespace Timelines.Infrastructure.Data.Repositories;

public class TimelineRepository(ITimelinesDbContext dbContext) : ITimelinesRepository
{
    public async Task<Timeline> GetTimelineByIdAsync(TimelineId timelineId, CancellationToken cancellationToken)
    {
        return await dbContext.Timelines
                   .AsNoTracking()
                   .SingleOrDefaultAsync(t => t.Id == timelineId, cancellationToken) ??
               throw new TimelineNotFoundException(timelineId.ToString());
    }

    public async Task UpdateTimelineAsync(Timeline timeline, CancellationToken cancellationToken)
    {
        dbContext.Timelines.Update(timeline);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
