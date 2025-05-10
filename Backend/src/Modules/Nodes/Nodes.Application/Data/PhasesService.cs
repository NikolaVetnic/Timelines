using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Nodes.Phase.Dtos;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;
using Mapster;
using Nodes.Application.Data.Abstractions;

namespace Nodes.Application.Data;

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
}
