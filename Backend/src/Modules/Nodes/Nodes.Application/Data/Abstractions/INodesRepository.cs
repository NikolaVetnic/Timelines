namespace Nodes.Application.Data.Abstractions;

public interface INodesRepository
{
    Task<Node> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task UpdateNodeAsync(Node node, CancellationToken cancellationToken);
}
