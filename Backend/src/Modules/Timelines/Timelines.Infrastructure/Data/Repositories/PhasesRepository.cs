using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Phases.Exceptions;

namespace Timelines.Infrastructure.Data.Repositories;

public class PhasesRepository(ITimelinesDbContext dbContext) : IPhasesRepository
{
    public async Task CreatePhaseAsync(Phase phase, CancellationToken cancellationToken)
    {
        dbContext.Phases.Add(phase);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Phase> GetPhaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken)
    {
        return await dbContext.Phases
                   .AsNoTracking()
                   .SingleOrDefaultAsync(n => n.Id == phaseId, cancellationToken) ??
               throw new PhaseNotFoundException(phaseId.ToString());
    }

    public async Task<IEnumerable<Phase>> GetPhasesBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
                dbContext.Phases
                    .AsNoTracking()
                    .AsEnumerable()
                    .Where(t => t.NodeIds.Any(nodeId => nodeIds.Contains(nodeId)))
                    .ToList(),
            cancellationToken);
    }
}
