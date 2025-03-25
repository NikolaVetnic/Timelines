using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Nodes.Application.Data.Abstractions;
using Nodes.Application.Entities.Nodes.Exceptions;

namespace Nodes.Infrastructure.Data.Repositories;

public class NodesRepository(INodesDbContext dbContext) : INodesRepository
{
    public async Task<List<Node>> ListNodesPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        return await dbContext.Nodes
            .AsNoTracking()
            .OrderBy(n => n.Timestamp)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Node> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Nodes
                   .AsNoTracking()
                   .SingleOrDefaultAsync(n => n.Id == nodeId, cancellationToken) ??
               throw new NodeNotFoundException(nodeId.ToString());
    }

    public async Task<long> NodeCountAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Nodes.LongCountAsync(cancellationToken);
    }

    public async Task UpdateNodeAsync(Node node, CancellationToken cancellationToken = default)
    {
        dbContext.Nodes.Update(node);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

    public async Task RemoveNode(Node node, CancellationToken cancellationToken)
    {
        dbContext.Nodes.Remove(node);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Node>> GetNodesBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken)
    {
        {
            return await dbContext.Nodes
            .AsNoTracking()
                .Where(n => timelineIds.Contains(n.TimelineId))
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
