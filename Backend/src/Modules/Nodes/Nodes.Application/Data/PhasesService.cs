using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Phase.Dtos;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;
using Nodes.Application.Data.Abstractions;

namespace Nodes.Application.Data;

public class PhasesService(IServiceProvider serviceProvider, IPhasesRepository phasesRepository) : IPhasesService
{
    public async Task<PhaseDto> GetPhaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken)
    {
        return await phasesRepository.GetPhaseByIdAsync(phaseId, cancellationToken);
    }

    public async Task<PhaseBaseDto> GetPhaseBaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken)
    {
        return await phasesRepository.GetPhaseBaseByIdAsync(phaseId, cancellationToken);
    }
}
