using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Application.Entities.Timelines.Queries.ListNodesByTimelineId;

public record ListNodesByTimelineIdQuery(TimelineId Id, PaginationRequest PaginationRequest) : IQuery<ListNodesByTimelineIdResult>
{
    public ListNodesByTimelineIdQuery(string id, PaginationRequest paginationRequest)
        : this(TimelineId.Of(Guid.Parse(id)), paginationRequest) { }
}

public record ListNodesByTimelineIdResult(PaginatedResult<NodeBaseDto> Nodes);
