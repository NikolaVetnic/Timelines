using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.Dtos;

namespace Timelines.Application.Entities.Timelines.Queries.ListNodesByTimelineId;

public class ListNodesByTimelineIdHandler(INodesService nodesService) : IQueryHandler<ListNodesByTimelineIdQuery, ListNodesByTimelineIdResult>
{
    public async Task<ListNodesByTimelineIdResult> Handle(ListNodesByTimelineIdQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await nodesService.CountNodesByTimelineIdAsync(query.Id, cancellationToken);

        var nodes = await nodesService.ListNodesByTimelineIdPaginated(query.Id, pageIndex, pageSize, cancellationToken);

        return new ListNodesByTimelineIdResult(
            new PaginatedResult<NodeBaseDto>(
                pageIndex,
                pageSize,
                totalCount,
                nodes));
    }
}
