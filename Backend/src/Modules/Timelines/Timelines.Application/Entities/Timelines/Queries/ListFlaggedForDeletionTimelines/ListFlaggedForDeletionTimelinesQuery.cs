using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

namespace Timelines.Application.Entities.Timelines.Queries.ListFlaggedForDeletionTimelines;

public record ListFlaggedForDeletionTimelinesQuery(PaginationRequest PaginationRequest) : IQuery<ListFlaggedForDeletionTimelinesResult>;

public record ListFlaggedForDeletionTimelinesResult(PaginatedResult<TimelineDto> Timelines);
