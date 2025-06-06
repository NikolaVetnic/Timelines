using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

namespace Timelines.Application.Entities.Timelines.Queries.ListFlaggedForDeletionTimelines;

public record ListFlaggedForDeletionTimelinesQuery(PaginationRequest PaginationRequest) : IQuery<ListFlaggedForDeletionTimelinesResponse>;

public record ListFlaggedForDeletionTimelinesResponse(PaginatedResult<TimelineDto> Timelines);
