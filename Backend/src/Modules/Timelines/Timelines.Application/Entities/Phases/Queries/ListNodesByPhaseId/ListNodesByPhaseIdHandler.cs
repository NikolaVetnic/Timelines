using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.Dtos;

namespace Timelines.Application.Entities.Phases.Queries.ListNodesByPhaseId;

public class ListNodesByPhaseIdHandler(IPhasesService phasesService, INodesService nodesService) : IQueryHandler<ListNodesByPhaseIdQuery, ListNodesByPhaseIdResult>
{
    public async Task<ListNodesByPhaseIdResult> Handle(ListNodesByPhaseIdQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var phase = await phasesService.GetPhaseBaseByIdAsync(query.Id, cancellationToken);

        var totalCount = await nodesService.CountNodesBelongingToPhase(phase.StartDate, phase.EndDate, cancellationToken);

        var nodes = await nodesService.ListNodesBelongingToPhasePaginated(phase.StartDate, phase.EndDate, pageIndex, pageSize, cancellationToken);

        return new ListNodesByPhaseIdResult(
            new PaginatedResult<NodeBaseDto>(
                pageIndex,
                pageSize,
                totalCount,
                nodes));
    }
}
