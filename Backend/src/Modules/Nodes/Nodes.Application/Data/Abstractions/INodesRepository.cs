using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace Nodes.Application.Data.Abstractions;

public interface INodesRepository
{
    Task<List<Node>> ListNodesPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<Node> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task<long> NodeCountAsync(CancellationToken cancellationToken);
    Task UpdateNodeAsync(Node node, CancellationToken cancellationToken);
}
