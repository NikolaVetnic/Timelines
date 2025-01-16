using BuildingBlocks.Application.Pagination;
using Timelines.Application.Entities.Timelines.Extensions;

namespace Timelines.Application.Entities.Timelines.Queries.ListTimelines;

public class ListTimelinesHandler(ITimelinesDbContext dbContext) : IQueryHandler<ListTimelinesQuery, ListTimelinesResult>
{
    public async Task<ListTimelinesResult> Handle(ListTimelinesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Timelines.LongCountAsync(cancellationToken);

        var nodes = await dbContext.Timelines
            .AsNoTracking()
            .OrderBy(n => n.CreatedAt)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        return new ListTimelinesResult(
            new PaginatedResult<TimelineDto>(
                pageIndex,
                pageSize,
                totalCount,
                nodes.ToTimelineDtoList()));
    }
}
