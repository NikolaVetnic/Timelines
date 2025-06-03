using BuildingBlocks.Application.Data;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Phases.Extensions;

namespace Timelines.Application.Entities.Phases.Queries.GetPhaseById;

internal class GetPhaseByIdHandler(IPhasesRepository phasesRepository, ITimelinesService timelinesService) : IQueryHandler<GetPhaseByIdQuery, GetPhaseByIdResult>
{
    public async Task<GetPhaseByIdResult> Handle(GetPhaseByIdQuery query, CancellationToken cancellationToken)
    {
        var phase = await phasesRepository.GetPhaseByIdAsync(query.Id, cancellationToken);
        var timelines = await timelinesService.GetTimelineByIdAsync(phase.TimelineId, cancellationToken);

        return new GetPhaseByIdResult(phase.ToPhaseDto(timelines));
    }
}
