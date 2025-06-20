using System.Linq.Expressions;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Nodes.Application.Data.Abstractions;

public interface INodesRepository
{
    Task<List<Node>> ListNodesPaginatedAsync(int pageIndex, int pageSize, Expression<Func<Node, bool>> predicate, CancellationToken cancellationToken);
    Task<long> CountNodesAsync(Expression<Func<Node, bool>> predicate, CancellationToken cancellationToken);

    Task<Node> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task<Node> GetTrackedNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task<List<Node>> GetNodesByIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);

    Task CreateNodeAsync(Node node, CancellationToken cancellationToken);
    Task UpdateNodeAsync(Node node, CancellationToken cancellationToken);
    Task DeleteNode(NodeId nodeId, CancellationToken cancellationToken);
    Task DeleteNodes(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);

    Task ReviveNode(NodeId nodeId, CancellationToken cancellationToken);

    Task<IEnumerable<Node>> GetNodesBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken);
}
