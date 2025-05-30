using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Timelines.Phase.Dtos;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Phases.Extensions;

namespace Timelines.Application.Data;

public class PhasesService(IServiceProvider serviceProvider, IPhasesRepository phasesRepository) : IPhasesService
{
    private ITimelinesService TimelinesService => serviceProvider.GetRequiredService<ITimelinesService>();

    public async Task<List<PhaseDto>> ListPhasesPaginated(int pageIndex, int pageSize,
        CancellationToken cancellationToken)
    {
        var timelines = await TimelinesService.ListTimelinesPaginated(pageIndex, pageSize, cancellationToken);

        var phases = await phasesRepository.ListPhasesPaginatedAsync(pageIndex, pageSize, cancellationToken);

        var phasesDtos = phases.Select(p =>
            p.ToPhaseDto(
                timelines.First(t => t.Id == p.TimelineId.ToString())
            )).ToList();

        return phasesDtos;
    }

    public async Task<PhaseBaseDto> GetPhaseBaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken)
    {
        var phase = await phasesRepository.GetPhaseByIdAsync(phaseId, cancellationToken);
        var phaseDto = phase.Adapt<PhaseBaseDto>();

        return phaseDto;
    }

    public async Task<PhaseDto> GetPhaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken)
    {
        var phase = await phasesRepository.GetPhaseByIdAsync(phaseId, cancellationToken);
        var phaseDto = phase.Adapt<PhaseDto>();

        var timeline = await TimelinesService.GetTimelineBaseByIdAsync(phase.TimelineId, cancellationToken);
        phaseDto.Timeline = timeline;

        return phaseDto;
    }

    public async Task<List<PhaseBaseDto>> GetPhasesByIdsAsync(IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken)
    {
        return (await phasesRepository.GetPhasesByIdsAsync(phaseIds, cancellationToken)).Select(n => n.ToPhaseBaseDto()).ToList();
    }

    public async Task<List<PhaseBaseDto>> GetPhasesBaseBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken)
    {
        var phases = await phasesRepository.GetPhasesBelongingToTimelineIdsAsync(timelineIds, cancellationToken);
        var phaseBaseDtos = phases.Adapt<List<PhaseBaseDto>>();
        return phaseBaseDtos;
    }

    public async Task<long> CountPhasesAsync(CancellationToken cancellationToken)
    {
        return await phasesRepository.PhaseCountAsync(cancellationToken); 
    }

    public async Task DeletePhase(PhaseId phaseId, CancellationToken cancellationToken)
    {
        var phase = await phasesRepository.GetPhaseByIdAsync(phaseId, cancellationToken);

        await TimelinesService.RemovePhase(phase.TimelineId, phaseId, cancellationToken);

        await phasesRepository.DeletePhaseAsync(phaseId, cancellationToken);
    }

    public async Task DeletePhases(TimelineId timelineId, IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken)
    {
        var input = phaseIds.ToList();

        await phasesRepository.DeletePhases(input, cancellationToken);
    }
}
