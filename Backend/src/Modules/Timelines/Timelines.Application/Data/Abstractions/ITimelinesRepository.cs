using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Application.Data.Abstractions;

public interface ITimelinesRepository
{
    Task<List<Timeline>> ListTimelinesPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<Timeline>> ListFlaggedForDeletionTimelinesPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<long> TimelineCountAsync(CancellationToken cancellationToken);

    Task<Timeline> GetTimelineByIdAsync(TimelineId timelineId, CancellationToken cancellationToken);
    Task<Timeline> GetTrackedTimelineByIdAsync(TimelineId timelineId, CancellationToken cancellationToken);

    Task CreateTimelineAsync(Timeline timeline, CancellationToken cancellationToken);
    Task UpdateTimelineAsync(Timeline timeline, CancellationToken cancellationToken);
    Task DeleteTimeline(TimelineId timelineId, CancellationToken cancellationToken);

    Task<IEnumerable<Timeline>> GetTimelinesBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
}
