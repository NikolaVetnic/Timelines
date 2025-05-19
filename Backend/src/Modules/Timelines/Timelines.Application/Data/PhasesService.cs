using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using BuildingBlocks.Domain.Timelines.Phase.Dtos;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Mapster;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Phases.Extensions;

namespace Timelines.Application.Data;

public class PhasesService(IServiceProvider serviceProvider, IPhasesRepository phasesRepository) : IPhasesService
{
    public async Task<PhaseBaseDto> GetPhaseBaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken)
    {
        var phase = await phasesRepository.GetPhaseByIdAsync(phaseId, cancellationToken);

        var phaseDto = phase.Adapt<PhaseBaseDto>();

        return phaseDto;
    }

    public async Task<List<PhaseBaseDto>> GetPhasesBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        var phases = await phasesRepository.GetPhasesBelongingToNodeIdsAsync(nodeIds, cancellationToken);
        var phaseBaseDtos = phases.Adapt<List<PhaseBaseDto>>();
        return phaseBaseDtos;
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
}
