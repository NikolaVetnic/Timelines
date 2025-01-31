using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

// ReSharper disable ClassNeverInstantiated.Global

namespace Timelines.Application.Entities.Timelines.Queries.ListTimelines;

public record ListTimelinesQuery(PaginationRequest PaginationRequest) : IQuery<ListTimelinesResult>;

public record ListTimelinesResult(PaginatedResult<TimelineDto> Timelines);
