using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

namespace Timelines.Application.Entities.Timelines.Queries.ListTimelines;

internal class ListTimelinesHandler(ITimelinesService timelineService)
    : IQueryHandler<ListTimelinesQuery, ListTimelinesResult>
{
    public async Task<ListTimelinesResult> Handle(ListTimelinesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var timelines = await timelineService.ListTimelinesPaginated(pageIndex, pageSize, cancellationToken);

        return new ListTimelinesResult(
            new PaginatedResult<TimelineDto>(
                pageIndex,
                pageSize,
                timelines.Count,
                timelines));
    }
}
