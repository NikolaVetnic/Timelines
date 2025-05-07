using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;
using Nodes.Application.Data.Abstractions;

namespace Nodes.Infrastructure.Data.Repositories;

public class PhasesRepository : IPhasesRepository
{
    public async Task<List<Phase>> ListPhasesPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<long> PhaseCountAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Phase> GetPhaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Phase>> GetPhasesByIdsAsync(IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdatePhaseAsync(Phase phase, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeletePhase(PhaseId phaseId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeletePhases(IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeletePhasesByNodeIds(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Phase>> GetPhasesBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
