using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

namespace Timelines.Application.Entities.Timelines.Queries.ListFlaggedForDeletionTimelines;

internal class ListFlaggedForDeletionTimelinesHandler(ITimelinesService timelinesService) : IQueryHandler<ListFlaggedForDeletionTimelinesQuery, ListFlaggedForDeletionTimelinesResponse>
{
    public async Task<ListFlaggedForDeletionTimelinesResponse> Handle(ListFlaggedForDeletionTimelinesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var timelines = await timelinesService.ListFlaggedForDeletionTimelinesPaginated(pageIndex, pageSize, cancellationToken);

        return new ListFlaggedForDeletionTimelinesResponse(
            new PaginatedResult<TimelineDto>(
                pageIndex,
                pageSize,
                timelines.Count,
                timelines));
    }
}
