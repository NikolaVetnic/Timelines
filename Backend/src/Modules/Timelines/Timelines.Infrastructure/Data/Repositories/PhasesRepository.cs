using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Phases.Exceptions;

namespace Timelines.Infrastructure.Data.Repositories;

public class PhasesRepository(ICurrentUser currentUser, ITimelinesDbContext dbContext) : IPhasesRepository
{
    public async Task<List<Phase>> ListPhasesPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.Phases
            .AsNoTracking()
            .OrderBy(p => p.CreatedBy)
            .Where(p => p.OwnerId == currentUser.UserId! && !p.IsDeleted)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task CreatePhaseAsync(Phase phase, CancellationToken cancellationToken)
    {
        dbContext.Phases.Add(phase);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Phase> GetPhaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken)
    {
        return await dbContext.Phases
                   .AsNoTracking()
                   .SingleOrDefaultAsync(p => p.Id == phaseId && !p.IsDeleted, cancellationToken) ??
               throw new PhaseNotFoundException(phaseId.ToString());
    }

    public async Task<List<Phase>> GetPhasesByIdsAsync(IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken)
    {
        return await dbContext.Phases
            .AsNoTracking()
            .Where(p => phaseIds.Contains(p.Id) && p.OwnerId == currentUser.UserId! && !p.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Phase>> GetPhasesBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken)
    {
        return await dbContext.Phases
            .AsNoTracking()
            .Where(p => timelineIds.Contains(p.TimelineId) && !p.IsDeleted)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<long> PhaseCountAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Phases
            .Where(p => p.OwnerId == currentUser.UserId!)
            .LongCountAsync(cancellationToken);
    }

    public async Task UpdatePhaseAsync(Phase phase, CancellationToken cancellationToken)
    {
        dbContext.Phases.Update(phase);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePhaseAsync(PhaseId phaseId, CancellationToken cancellationToken)
    {
        var phaseToDelete = await dbContext.Phases
            .FirstAsync(p => p.Id == phaseId, cancellationToken);

        phaseToDelete.MarkAsDeleted();

        dbContext.Phases.Update(phaseToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePhases(IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken)
    {
        var phasesToDelete = await dbContext.Phases
            .Where(p => phaseIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        foreach (var phase in phasesToDelete)
            phase.MarkAsDeleted();

        dbContext.Phases.UpdateRange(phasesToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
