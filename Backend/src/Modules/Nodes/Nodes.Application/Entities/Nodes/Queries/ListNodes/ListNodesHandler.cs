using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using Nodes.Application.Data.Abstractions;

namespace Nodes.Application.Entities.Nodes.Queries.ListNodes;

internal class ListNodesHandler(INodesDbContext dbContext, IPhasesService phasesService) : IQueryHandler<ListNodesQuery, ListNodesResult>
{
    // todo: Refactor so that Services are used
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

        var nodeDtos = nodes.Select(r =>
        {
            var node = phasesService.GetNodeBaseByIdAsync(r.NodeId, cancellationToken).GetAwaiter().GetResult();
            return r.ToReminderDto(node);
        }).ToList();

        return new ListNodesResult(
            new PaginatedResult<NodeDto>(
                pageIndex,
                pageSize,
                totalCount,
                nodeDtos));
    }
}
