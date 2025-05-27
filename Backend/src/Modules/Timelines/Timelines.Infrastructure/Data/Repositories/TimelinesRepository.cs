using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Timelines.Exceptions;

namespace Timelines.Infrastructure.Data.Repositories;

public class TimelinesRepository(ICurrentUser currentUser, ITimelinesDbContext dbContext) : ITimelinesRepository
{
    #region List

    public async Task<List<Timeline>> ListTimelinesPaginatedAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken)
    {
        return await dbContext.Timelines
            .AsNoTracking()
            .Where(t => t.OwnerId == currentUser.UserId! && t.IsDeleted == false)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<Timeline>> ListFlaggedForDeletionTimelinesPaginatedAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken)
    {
        return await dbContext.Timelines
            .AsNoTracking()
            .Where(t => t.OwnerId == currentUser.UserId! && t.IsDeleted)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<long> TimelineCountAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Timelines
            .Where(t => t.OwnerId == currentUser.UserId! && !t.IsDeleted)
            .LongCountAsync(cancellationToken);
    }

    #endregion

    #region Get

    public async Task<Timeline> GetTimelineByIdAsync(TimelineId timelineId, CancellationToken cancellationToken)
    {
        return await dbContext.Timelines
                   .AsNoTracking()
                   .SingleOrDefaultAsync(t => t.Id == timelineId && t.OwnerId == currentUser.UserId!, cancellationToken) ??
               throw new TimelineNotFoundException(timelineId.ToString());
    }

    public async Task<Timeline> GetTrackedTimelineByIdAsync(TimelineId timelineId, CancellationToken cancellationToken)
    {
        return await dbContext.Timelines
                   .SingleOrDefaultAsync(t => t.Id == timelineId, cancellationToken) ??
               throw new TimelineNotFoundException(timelineId.ToString());
    }

    #endregion

    public async Task CreateTimelineAsync(Timeline timeline, CancellationToken cancellationToken)
    {
        dbContext.Timelines.Add(timeline);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTimelineAsync(Timeline timeline, CancellationToken cancellationToken = default)
    {
        dbContext.Timelines.Update(timeline);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTimeline(TimelineId timelineId, CancellationToken cancellationToken)
    {
        var timelineToDelete = await dbContext.Timelines
            .FirstAsync(t => t.Id == timelineId && t.OwnerId == currentUser.UserId!, cancellationToken);

        timelineToDelete.MarkAsDeleted();

        dbContext.Timelines.Update(timelineToDelete);
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
                    .Where(t => t.NodeIds.Any(nodeIds.Contains) && t.OwnerId == currentUser.UserId! && !t.IsDeleted)
                    .ToList(),
            cancellationToken);
    }

    #endregion
}
