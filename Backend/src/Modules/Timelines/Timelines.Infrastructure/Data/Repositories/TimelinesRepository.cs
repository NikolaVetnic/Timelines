﻿using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Timelines.Exceptions;

namespace Timelines.Infrastructure.Data.Repositories;

public class TimelinesRepository(ITimelinesDbContext dbContext) : ITimelinesRepository
{
    #region List
    public async Task<List<Timeline>> ListTimelinessPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.Timelines
            .AsNoTracking()
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<long> TimelineCountAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Timelines.LongCountAsync(cancellationToken);
    }
    #endregion

    #region Get
    public async Task<Timeline> GetTimelineByIdAsync(TimelineId timelineId, CancellationToken cancellationToken)
    {
        return await dbContext.Timelines
                   .AsNoTracking()
                   .SingleOrDefaultAsync(t => t.Id == timelineId, cancellationToken) ??
               throw new TimelineNotFoundException(timelineId.ToString());
    }
    #endregion
    
    public async Task UpdateTimelineAsync(Timeline timeline, CancellationToken cancellationToken)
    {
        dbContext.Timelines.Update(timeline);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTimeline(TimelineId timelineId, CancellationToken cancellationToken)
    {
        var timelineToDelete = await dbContext.Timelines
            .FirstAsync(t => t.Id == timelineId, cancellationToken);
        
        dbContext.Timelines.Remove(timelineToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    #region Relationships
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
    #endregion
}
