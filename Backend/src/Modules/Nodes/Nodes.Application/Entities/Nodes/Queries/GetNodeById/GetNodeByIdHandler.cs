using Nodes.Application.Data;
using Nodes.Application.Entities.Nodes.Dtos;
using Nodes.Application.Entities.Nodes.Exceptions;

namespace Nodes.Application.Entities.Nodes.Queries.GetNodeById;

public class GetNodeByIdHandler(INodesDbContext dbContext) : IQueryHandler<GetNodeByIdQuery, GetNodeByIdResult>
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

internal static partial class NodeExtensions
{
    public static NodeDto ToNodeDto(this Node node)
    {
        return new NodeDto(
            node.Id.ToString(),
            node.Title,
            node.Description,
            node.Timestamp,
            node.Importance,
            node.Phase,
            node.Categories.ToList(),
            node.Tags.ToList());
    }
}
