using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.Phase.Dtos;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Phases.Extensions;

namespace Timelines.Application.Entities.Phases.Queries.ListPhases;

internal class ListPhasesHandler(IPhasesRepository phasesRepository, ITimelinesService timelineService)
    : IQueryHandler<ListPhasesQuery, ListPhasesResult>
{
    public async Task<ListPhasesResult> Handle(ListPhasesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var phases = await phasesRepository.ListPhasesPaginatedAsync(p => !p.IsDeleted, pageIndex, pageSize, cancellationToken);
        var timeline = await timelineService.GetTimelineBaseByIdAsync(phases[0].TimelineId, cancellationToken);

        var phaseDtos = phases.Select(p => p.ToPhaseDto(timeline)).ToList();

        return new ListPhasesResult(
            new PaginatedResult<PhaseDto>(
                pageIndex,
                pageSize,
                phases.Count,
                phaseDtos));
    }
}
