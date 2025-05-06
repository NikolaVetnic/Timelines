using Nodes.Application.Data.Abstractions.Phases;

namespace Nodes.Infrastructure.Data.Repositories;

public class PhasesRepository(IPhasesDbContext dbContext) : IPhasesRepository
{
    public async Task CreatePhaseAsync(Phase phase, CancellationToken cancellationToken)
    {
        dbContext.Phases.Add(phase);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
