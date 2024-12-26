namespace Nodes.Application.Entities.Nodes.Queries.ListNodes;

public class ListNodesHandler(INodesDbContext dbContext) : IQueryHandler<ListNodesQuery, ListNodesResult>
{
    public async Task<ListNodesResult> Handle(ListNodesQuery query, CancellationToken cancellationToken)
    {
        var nodes = await dbContext.Nodes
            .AsNoTracking()
            .OrderBy(n => n.Timestamp)
            .ToListAsync(cancellationToken: cancellationToken);
        
        return new ListNodesResult(nodes);
    }
}
