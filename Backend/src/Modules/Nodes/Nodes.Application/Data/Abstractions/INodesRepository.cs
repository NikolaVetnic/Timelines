using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Nodes.Application.Data.Abstractions;

public interface INodesRepository
{
    Task<List<Node>> ListNodesPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<Node>> ListFlaggedForDeletionNodesPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<Node>> ListNodesByTimelineIdPaginatedAsync(TimelineId timelineId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<Node>> ListNodesBelongingToPhaseAsync(DateTime startDate, DateTime? endDate, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<long> CountAllNodesAsync(CancellationToken cancellationToken);
    Task<long> CountAllNodesByTimelineIdAsync(TimelineId timelineId, CancellationToken cancellationToken);
    Task<long> NodeCountBelongingToPhase(DateTime startDate, DateTime? endDate, CancellationToken cancellationToken);

    Task<Node> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task<Node> GetTrackedNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task<List<Node>> GetNodesByIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);

    Task CreateNodeAsync(Node node, CancellationToken cancellationToken);
    Task UpdateNodeAsync(Node node, CancellationToken cancellationToken);
    Task DeleteNode(NodeId nodeId, CancellationToken cancellationToken);
    Task DeleteNodes(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
    
    Task<IEnumerable<Node>> GetNodesBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken);
}
