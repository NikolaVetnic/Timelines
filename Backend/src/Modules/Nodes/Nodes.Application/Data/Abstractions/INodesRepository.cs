using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Nodes.Application.Data.Abstractions;

public interface INodesRepository
{
    Task<Node> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task UpdateNodeAsync(Node node, CancellationToken cancellationToken);
    Task RemoveNode(Node node, CancellationToken cancellationToken);
    Task<IEnumerable<Node>> GetNodesBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken);
}
