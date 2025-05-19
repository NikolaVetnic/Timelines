using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
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
            .Where(p => p.OwnerId == currentUser.UserId!)
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

    public async Task<List<Phase>> GetPhasesByIdsAsync(IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken)
    {
        return await dbContext.Phases
            .AsNoTracking()
            .Where(n => phaseIds.Contains(n.Id) && n.OwnerId == currentUser.UserId!)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Phase>> GetPhasesBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken)
    {
        return await dbContext.Phases
            .AsNoTracking()
            .Where(n => timelineIds.Contains(n.TimelineId))
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<long> PhaseCountAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Phases
            .Where(n => n.OwnerId == currentUser.UserId!)
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

        dbContext.Phases.Remove(phaseToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
