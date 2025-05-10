using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;

namespace Nodes.Application.Data.Abstractions;

public interface IPhasesRepository
{
    Task<Phase> GetPhaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken);
    Task<IEnumerable<Phase>> GetPhasesBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
}
