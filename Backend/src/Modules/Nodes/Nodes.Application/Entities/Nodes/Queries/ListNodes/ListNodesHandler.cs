using BuildingBlocks.Application.Pagination;
using Nodes.Application.Entities.Nodes.Dtos;
using Nodes.Application.Entities.Nodes.Extensions;

namespace Nodes.Application.Entities.Nodes.Queries.ListNodes;

public class ListNodesHandler(INodesDbContext dbContext) : IQueryHandler<ListNodesQuery, ListNodesResult>
{
    public async Task<ListNodesResult> Handle(ListNodesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Nodes.LongCountAsync(cancellationToken);
        
        var nodes = await dbContext.Nodes
            .AsNoTracking()
            .OrderBy(n => n.Timestamp)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
        
        return new ListNodesResult(
            new PaginatedResult<NodeDto>(
                pageIndex,
                pageSize,
                totalCount,
                nodes.ToNodeDtoList()));
    }
}
