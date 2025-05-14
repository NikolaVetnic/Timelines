using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Api.Controllers.Timelines;

public record ListTimelinesResponse(PaginatedResult<TimelineDto> Timelines);

public record GetTimelineByIdResponse(TimelineDto Timeline);

public record ListNodesByTimelineIdResponse(PaginatedResult<NodeDto> Nodes);

public record CreateTimelineResponse(TimelineId Id);

public record CreateTimelineWithTemplateResponse(TimelineBaseDto Timeline);

public record UpdateTimelineResponse(TimelineBaseDto Timeline);

public record DeleteTimelineResponse(bool TimelineDeleted);
