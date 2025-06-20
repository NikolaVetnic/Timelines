using System.Linq.Expressions;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Nodes.Application.Data.Abstractions;
using Nodes.Application.Entities.Nodes.Exceptions;

namespace Nodes.Infrastructure.Data.Repositories;

public class NodesRepository(ICurrentUser currentUser, INodesDbContext dbContext) : INodesRepository
{
    #region List

    public async Task<List<Node>> ListNodesPaginatedAsync(
        int pageIndex,
        int pageSize,
        Expression<Func<Node, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Nodes
            .AsNoTracking()
            .OrderBy(n => n.Timestamp)
            .Where(n => n.OwnerId == currentUser.UserId)
            .Where(predicate)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<long> CountNodesAsync(Expression<Func<Node, bool>> predicate, CancellationToken cancellationToken)
    {
        return await dbContext.Nodes
            .Where(n => n.OwnerId == currentUser.UserId)
            .Where(predicate)
            .LongCountAsync(cancellationToken);
    }

    #endregion

    #region Get
    public async Task<Node> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Nodes
                   .AsNoTracking()
                   .SingleOrDefaultAsync(n => n.Id == nodeId && n.OwnerId == currentUser.UserId!, cancellationToken) ??
               throw new NodeNotFoundException(nodeId.ToString());
    }

    public async Task<Node> GetTrackedNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        return await dbContext.Nodes
                   .SingleOrDefaultAsync(n => n.Id == nodeId, cancellationToken) ??
               throw new NodeNotFoundException(nodeId.ToString());
    }

    public async Task<List<Node>> GetNodesByIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        return await dbContext.Nodes
            .AsNoTracking()
            .Where(n => nodeIds.Contains(n.Id) && n.OwnerId == currentUser.UserId! && !n.IsDeleted)
            .ToListAsync(cancellationToken);
    }
    #endregion

    public async Task CreateNodeAsync(Node node, CancellationToken cancellationToken = default)
    {
        dbContext.Nodes.Add(node);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateNodeAsync(Node node, CancellationToken cancellationToken = default)
    {
        dbContext.Nodes.Update(node);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteNode(NodeId nodeId, CancellationToken cancellationToken)
    {
        var nodeToDelete = await dbContext.Nodes
            .FirstAsync(n => n.Id == nodeId, cancellationToken);

        nodeToDelete.MarkAsDeleted();

        dbContext.Nodes.Update(nodeToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteNodes(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        var nodesToDelete = await dbContext.Nodes
            .Where(n => nodeIds.Contains(n.Id))
            .ToListAsync(cancellationToken);

        foreach (var node in nodesToDelete)
        {
            node.MarkAsDeleted();
        }

        dbContext.Nodes.UpdateRange(nodesToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ReviveNode(NodeId nodeId, CancellationToken cancellationToken = default)
    {
        var nodeToDelete = await dbContext.Nodes
            .FirstAsync(n => n.Id == nodeId, cancellationToken);

        nodeToDelete.Revive();

        dbContext.Nodes.Update(nodeToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    #region Relationships

    public async Task<IEnumerable<Node>> GetNodesBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken)
    {
        return await dbContext.Nodes
            .AsNoTracking()
            .Where(n => timelineIds.Contains(n.TimelineId))
            .ToListAsync(cancellationToken: cancellationToken);
    }

    #endregion
}
