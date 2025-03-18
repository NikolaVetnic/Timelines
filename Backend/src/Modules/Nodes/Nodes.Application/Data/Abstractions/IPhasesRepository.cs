using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;

namespace Nodes.Application.Data.Abstractions;

public interface IPhasesRepository
{
    Task<Phase> GetPhaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken);
    Task UpdatePhaseAsync(Phase phase, CancellationToken cancellationToken);
}
