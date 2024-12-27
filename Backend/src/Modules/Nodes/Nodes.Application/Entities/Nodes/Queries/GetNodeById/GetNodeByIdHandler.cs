using Nodes.Application.Entities.Nodes.Exceptions;
using Nodes.Application.Entities.Nodes.Extensions;

namespace Nodes.Application.Entities.Nodes.Queries.GetNodeById;

internal class GetNodeByIdHandler(INodesDbContext dbContext) : IQueryHandler<GetNodeByIdQuery, GetNodeByIdResult>
{
    public async Task<GetNodeByIdResult> Handle(GetNodeByIdQuery request, CancellationToken cancellationToken)
    {
        var node = await dbContext.Nodes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == NodeId.Of(Guid.Parse(request.Id)), cancellationToken);

        if (node is null)
            throw new NodeNotFoundException(request.Id);
        
        return new GetNodeByIdResult(node.ToNodeDto());
    }
}
