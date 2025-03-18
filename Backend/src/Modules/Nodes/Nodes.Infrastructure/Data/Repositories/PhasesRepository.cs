using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;
using Nodes.Application.Data.Abstractions;
using Nodes.Application.Entities.Phases.Exceptions;

namespace Nodes.Infrastructure.Data.Repositories;

public class PhasesRepository(IPhasesDbContext dbContext) : IPhasesRepository
{
    public async Task<Phase> GetPhaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken)
    {
        return await dbContext.Phases
                    .AsNoTracking()
                   .SingleOrDefaultAsync(p => p.Id == phaseId, cancellationToken) ??
               throw new PhaseNotFoundException(phaseId.ToString());
    }

    public async Task UpdatePhaseAsync(Phase phase, CancellationToken cancellationToken)
    {
        dbContext.Phases.Update(phase);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
