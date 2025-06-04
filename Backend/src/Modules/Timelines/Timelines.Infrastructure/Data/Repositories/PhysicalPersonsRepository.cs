using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.PhysicalPersons.Exceptions;

namespace Timelines.Infrastructure.Data.Repositories;

public class PhysicalPersonsRepository(ICurrentUser currentUser, ITimelinesDbContext dbContext) : IPhysicalPersonsRepository
{
    public async Task<List<PhysicalPerson>> ListPhysicalPersonsAsync(TimelineId timelineId, CancellationToken cancellationToken)
    {
        return await dbContext.PhysicalPersons
            .AsNoTracking()
            .OrderBy(p => p.CreatedBy)
            .Where(p => p.TimelineId == timelineId && p.OwnerId == currentUser.UserId && !p.IsDeleted)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<PhysicalPerson>> ListPhysicalPersonsPaginatedAsync(TimelineId timelineId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.PhysicalPersons
            .AsNoTracking()
            .OrderBy(p => p.CreatedBy)
            .Where(p => p.TimelineId == timelineId && p.OwnerId == currentUser.UserId! && !p.IsDeleted)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task CreatePhysicalPersonAsync(PhysicalPerson phase, CancellationToken cancellationToken)
    {
        dbContext.PhysicalPersons.Add(phase);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<PhysicalPerson> GetPhysicalPersonByIdAsync(PhysicalPersonId phaseId, CancellationToken cancellationToken)
    {
        return await dbContext.PhysicalPersons
                   .AsNoTracking()
                   .SingleOrDefaultAsync(p => p.Id == phaseId && !p.IsDeleted, cancellationToken) ??
               throw new PhysicalPersonNotFoundException(phaseId.ToString());
    }

    public async Task<List<PhysicalPerson>> GetPhysicalPersonsByIdsAsync(IEnumerable<PhysicalPersonId> phaseIds, CancellationToken cancellationToken)
    {
        return await dbContext.PhysicalPersons
            .AsNoTracking()
            .Where(p => phaseIds.Contains(p.Id) && p.OwnerId == currentUser.UserId! && !p.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PhysicalPerson>> GetPhysicalPersonsBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken)
    {
        return await dbContext.PhysicalPersons
            .AsNoTracking()
            .Where(p => timelineIds.Contains(p.TimelineId) && !p.IsDeleted)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<long> PhysicalPersonCountAsync(CancellationToken cancellationToken)
    {
        return await dbContext.PhysicalPersons
            .Where(p => p.OwnerId == currentUser.UserId!)
            .LongCountAsync(cancellationToken);
    }

    public async Task UpdatePhysicalPersonAsync(PhysicalPerson phase, CancellationToken cancellationToken)
    {
        dbContext.PhysicalPersons.Update(phase);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePhysicalPersonAsync(PhysicalPersonId phaseId, CancellationToken cancellationToken)
    {
        var phaseToDelete = await dbContext.PhysicalPersons
            .FirstAsync(p => p.Id == phaseId, cancellationToken);

        phaseToDelete.MarkAsDeleted();

        dbContext.PhysicalPersons.Update(phaseToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePhysicalPersons(IEnumerable<PhysicalPersonId> phaseIds, CancellationToken cancellationToken)
    {
        var phasesToDelete = await dbContext.PhysicalPersons
            .Where(p => phaseIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        foreach (var phase in phasesToDelete)
            phase.MarkAsDeleted();

        dbContext.PhysicalPersons.UpdateRange(phasesToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
