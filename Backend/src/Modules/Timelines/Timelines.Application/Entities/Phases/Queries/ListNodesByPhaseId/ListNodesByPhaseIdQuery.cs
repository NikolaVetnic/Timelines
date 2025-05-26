using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;

namespace Timelines.Application.Entities.Phases.Queries.ListNodesByPhaseId;

public record ListNodesByPhaseIdQuery(PhaseId Id, PaginationRequest PaginationRequest) : IQuery<ListNodesByPhaseIdResult>
{
    public ListNodesByPhaseIdQuery(string id, PaginationRequest paginationRequest)
        : this(PhaseId.Of(Guid.Parse(id)), paginationRequest) { }
}

public record ListNodesByPhaseIdResult(PaginatedResult<NodeBaseDto> Nodes);
