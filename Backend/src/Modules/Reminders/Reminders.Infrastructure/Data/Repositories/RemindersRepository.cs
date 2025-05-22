using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using Reminders.Application.Data.Abstractions;
using Reminders.Application.Entities.Reminders.Exceptions;

namespace Reminders.Infrastructure.Data.Repositories;

public class RemindersRepository(ICurrentUser currentUser, IRemindersDbContext dbContext) : IRemindersRepository
{
    public async Task AddReminderAsync(Reminder reminder, CancellationToken cancellationToken)
    {
        dbContext.Reminders.Add(reminder);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Reminder>> ListRemindersPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.Reminders
            .AsNoTracking()
            .OrderBy(r => r.NotifyAt)
            .Where(r => r.OwnerId == currentUser.UserId!)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<Reminder>> ListRemindersByNodeIdPaginatedAsync(NodeId nodeId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.Reminders
            .AsNoTracking()
            .Where(r => r.NodeId == nodeId && r.OwnerId == currentUser.UserId!)
            .OrderBy(r => r.CreatedAt)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Reminder> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken)
    {
        return await dbContext.Reminders
                   .AsNoTracking()
                   .SingleOrDefaultAsync(r => r.Id == reminderId && r.OwnerId == currentUser.UserId!, cancellationToken) ??
               throw new ReminderNotFoundException(reminderId.ToString());
    }

    public async Task<long> ReminderCountAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Reminders
            .Where(r => r.OwnerId == currentUser.UserId!)
            .LongCountAsync(cancellationToken);
    }

    public async Task<long> ReminderCountByNodeIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        return await dbContext.Reminders
            .Where(r => r.OwnerId == currentUser.UserId!)
            .LongCountAsync(r => r.NodeId == nodeId, cancellationToken);
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

        reminderToDelete.MarkAsDeleted();

        dbContext.Reminders.Update(reminderToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteReminders(IEnumerable<ReminderId> reminderIds, CancellationToken cancellationToken)
    {
        var remindersToDelete = await dbContext.Reminders
            .Where(r => reminderIds.Contains(r.Id))
            .ToListAsync(cancellationToken);

        foreach (var reminder in remindersToDelete)
        {
            reminder.MarkAsDeleted();
        }

        dbContext.Reminders.UpdateRange(remindersToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
