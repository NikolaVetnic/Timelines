using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;

namespace Nodes.Application.Data.Abstractions;

public interface IPhasesRepository
{
    Task<List<Phase>> ListPhasesPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<long> PhaseCountAsync(CancellationToken cancellationToken);

    Task<Phase> GetPhaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken);
    Task<List<Phase>> GetPhasesByIdsAsync(IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken);

    Task UpdatePhaseAsync(Phase phase, CancellationToken cancellationToken);
    Task DeletePhase(PhaseId phaseId, CancellationToken cancellationToken);
    Task DeletePhases(IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken);
    Task DeletePhasesByNodeIds(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);

    Task<IEnumerable<Phase>> GetPhasesBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
}
