using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.Phase.Dtos;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Phases.Extensions;

namespace Timelines.Application.Entities.Phases.Queries.ListFlaggedForDeletionPhases;

internal class ListFlaggedForDeletionPhasesHandler(ITimelinesService timelinesService, IPhasesRepository phasesRepository) : IQueryHandler<ListFlaggedForDeletionPhasesQuery, ListFlaggedForDeletionPhasesResult>
{
    public async Task<ListFlaggedForDeletionPhasesResult> Handle(ListFlaggedForDeletionPhasesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var phases = await phasesRepository.ListPhasesPaginatedAsync(p => p.IsDeleted, pageIndex, pageSize, cancellationToken);
        var timeline = await timelinesService.GetTimelineBaseByIdAsync(phases[0].TimelineId, cancellationToken);

        var phaseDtos = phases.Select(p => p.ToPhaseDto(timeline)).ToList();

        return new ListFlaggedForDeletionPhasesResult(
            new PaginatedResult<PhaseDto>(
                pageIndex,
                pageSize,
                phases.Count,
                phaseDtos));
    }
}
