using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.Dtos;

namespace Nodes.Application.Entities.Nodes.Queries.ListFlaggedForDeletionNodes;

internal class ListFlaggedForDeletionNodesHandler(INodesService nodesService) : IQueryHandler<ListFlaggedForDeletionNodesQuery, ListFlaggedForDeletionNodesResponse>
{
    public async Task<ListFlaggedForDeletionNodesResponse> Handle(ListFlaggedForDeletionNodesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var nodes = await nodesService.ListFlaggedForDeletionNodesPaginated(pageIndex, pageSize, cancellationToken);

        return new ListFlaggedForDeletionNodesResponse(
            new PaginatedResult<NodeDto>(
                pageIndex,
                pageSize,
                nodes.Count,
                nodes));
    }
}
