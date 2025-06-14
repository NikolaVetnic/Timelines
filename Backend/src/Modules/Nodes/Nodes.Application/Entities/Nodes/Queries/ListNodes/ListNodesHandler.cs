using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.Dtos;

namespace Nodes.Application.Entities.Nodes.Queries.ListNodes;

internal class ListNodesHandler(INodesService nodesService)
    : IQueryHandler<ListNodesQuery, ListNodesResult>
{
    public async Task<ListNodesResult> Handle(ListNodesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var nodes = await nodesService.ListNodesPaginated(pageIndex, pageSize, cancellationToken);

        return new ListNodesResult(
            new PaginatedResult<NodeDto>(
                pageIndex,
                pageSize,
                nodes.Count,
                nodes));
    }
}
