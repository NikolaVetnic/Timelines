using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.Phase.Dtos;

namespace Timelines.Application.Entities.Phases.Queries.ListPhases;

internal class ListPhasesHandler(IPhasesService phasesService)
    : IQueryHandler<ListPhasesQuery, ListPhasesResult>
{
    public async Task<ListPhasesResult> Handle(ListPhasesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await phasesService.CountPhasesAsync(cancellationToken);

        var phases = await phasesService.ListPhasesPaginated(pageIndex, pageSize, cancellationToken);

        return new ListPhasesResult(
            new PaginatedResult<PhaseDto>(
                pageIndex,
                pageSize,
                totalCount,
                phases));
    }
}
