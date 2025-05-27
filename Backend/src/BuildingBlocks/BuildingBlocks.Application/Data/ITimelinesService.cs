using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface ITimelinesService
{
    Task<List<TimelineDto>> ListTimelinesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<TimelineDto>> ListFlaggedForDeletionTimelinesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<TimelineDto> GetTimelineByIdAsync(TimelineId timelineId, CancellationToken cancellationToken);
    Task<TimelineBaseDto> GetTimelineBaseByIdAsync(TimelineId timelineId, CancellationToken cancellationToken);
    Task<long> CountTimelinesAsync(CancellationToken cancellationToken);
    Task EnsureTimelineBelongsToOwner(TimelineId timelineId, CancellationToken cancellationToken);

    Task AddNode(TimelineId timelineId, NodeId nodeId, CancellationToken cancellationToken);
    Task RemoveNode(TimelineId timelineId, NodeId nodeId, CancellationToken cancellationToken);
    Task RemoveNodes(TimelineId timelineId, IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);

    Task AddPhase(TimelineId timelineId, PhaseId phaseId, CancellationToken cancellationToken);
    Task RemovePhase(TimelineId timelineId, PhaseId phaseId, CancellationToken cancellationToken);
    Task RemovePhases(TimelineId timelineId, IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken);

    Task<List<TimelineBaseDto>> GetTimelinesBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
}
