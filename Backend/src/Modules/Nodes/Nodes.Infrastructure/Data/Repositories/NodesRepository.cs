using Nodes.Application.Data;
using Nodes.Application.Data.Abstractions;
using Nodes.Application.Entities.Nodes.Exceptions;

namespace Nodes.Infrastructure.Data.Repositories;

public class NodesRepository(INodesDbContext dbContext) : INodesRepository
{
    public async Task<Node> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Nodes
                   .AsNoTracking()
                   .SingleOrDefaultAsync(n => n.Id == nodeId, cancellationToken) ??
               throw new NodeNotFoundException(nodeId.ToString());
    }

    public async Task UpdateNodeAsync(Node node, CancellationToken cancellationToken = default)
    {
        dbContext.Nodes.Update(node);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
