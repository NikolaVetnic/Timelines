using Nodes.Application.Entities.Nodes.Exceptions;
using Nodes.Application.Entities.Nodes.Extensions;

namespace Nodes.Application.Entities.Nodes.Queries.GetNodeById;

internal class GetNodeByIdHandler(INodesDbContext dbContext) : IQueryHandler<GetNodeByIdQuery, GetNodeByIdResult>
{
    public async Task<GetNodeByIdResult> Handle(GetNodeByIdQuery query, CancellationToken cancellationToken)
    {
        var nodeId = query.Id.ToString();
        
        var node = await dbContext.Nodes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == NodeId.Of(Guid.Parse(nodeId)), cancellationToken);

        if (node is null)
            throw new NodeNotFoundException(nodeId);
        
        return new GetNodeByIdResult(node.ToNodeDto());
    }
}
