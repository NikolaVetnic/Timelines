using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using Reminders.Application.Data.Abstractions;
using Reminders.Application.Entities.Reminders.Exceptions;

namespace Reminders.Infrastructure.Data.Repositories;

public class RemindersRepository(IRemindersDbContext dbContext) : IRemindersRepository
{
    public async Task<List<Reminder>> ListRemindersPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.Reminders
            .AsNoTracking()
            .OrderBy(r => r.NotifyAt)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<Reminder>> ListRemindersByNodeIdPaginatedAsync(NodeId nodeId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.Reminders
            .AsNoTracking()
            .Where(r => r.NodeId == nodeId)
            .OrderBy(r => r.CreatedAt)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Reminder> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken)
    {
        return await dbContext.Reminders
                   .AsNoTracking()
                   .SingleOrDefaultAsync(r => r.Id == reminderId, cancellationToken) ??
               throw new ReminderNotFoundException(reminderId.ToString());
    }

    public async Task<long> ReminderCountAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Reminders.LongCountAsync(cancellationToken);
    }

    public async Task<long> ReminderCountByNodeIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        return await dbContext.Reminders.LongCountAsync(r => r.NodeId == nodeId, cancellationToken);
    }

    public async Task<IEnumerable<Reminder>> GetRemindersBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        return await dbContext.Reminders
            .AsNoTracking()
            .Where(r => nodeIds.Contains(r.NodeId))
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task UpdateReminderAsync(Reminder reminder, CancellationToken cancellationToken)
    {
        dbContext.Reminders.Update(reminder);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteReminder(ReminderId reminderId, CancellationToken cancellationToken)
    {
        var reminderToDelete = await dbContext.Reminders
            .FirstAsync(r => r.Id == reminderId, cancellationToken);

        dbContext.Reminders.Remove(reminderToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteReminders(IEnumerable<ReminderId> reminderIds, CancellationToken cancellationToken)
    {
        var remindersToDelete = await dbContext.Reminders
            .Where(r => reminderIds.Contains(r.Id))
            .ToListAsync(cancellationToken);

        dbContext.Reminders.RemoveRange(remindersToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
