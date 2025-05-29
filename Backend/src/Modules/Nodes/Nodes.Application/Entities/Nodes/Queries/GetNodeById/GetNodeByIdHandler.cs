using BuildingBlocks.Application.Data;
using Nodes.Application.Entities.Nodes.Exceptions;

namespace Nodes.Application.Entities.Nodes.Queries.GetNodeById;

internal class GetNodeByIdHandler(INodesService nodesService)
    : IQueryHandler<GetNodeByIdQuery, GetNodeByIdResult>
{
    public async Task<GetNodeByIdResult> Handle(GetNodeByIdQuery query, CancellationToken cancellationToken)
    {
        var nodeDto = await nodesService.GetNodeByIdAsync(query.Id, cancellationToken);

        if (nodeDto is null)
            throw new NodeNotFoundException(query.Id.ToString());
        
        return new GetNodeByIdResult(nodeDto);
    }
}
