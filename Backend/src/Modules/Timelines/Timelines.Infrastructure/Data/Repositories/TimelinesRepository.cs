using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Timelines.Exceptions;

namespace Timelines.Infrastructure.Data.Repositories;

public class TimelinesRepository(ITimelinesDbContext dbContext) : ITimelinesRepository
{
    public async Task<List<Timeline>> ListTimelinessPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.Timelines
            .AsNoTracking()
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Timeline> GetTimelineByIdAsync(TimelineId timelineId, CancellationToken cancellationToken)
    {
        return await dbContext.Timelines
                   .AsNoTracking()
                   .SingleOrDefaultAsync(t => t.Id == timelineId, cancellationToken) ??
               throw new TimelineNotFoundException(timelineId.ToString());
    }

    public async Task<long> TimelineCountAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Timelines.LongCountAsync(cancellationToken);
    }

    public async Task UpdateTimelineAsync(Timeline timeline, CancellationToken cancellationToken)
    {
        dbContext.Timelines.Update(timeline);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Timeline>> GetTimelinesBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds,
        CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
                dbContext.Timelines
                    .AsNoTracking()
                    .AsEnumerable()
                    .Where(t => t.NodeIds.Any(nodeId => nodeIds.Contains(nodeId)))
                    .ToList(),
            cancellationToken);
    }
}
