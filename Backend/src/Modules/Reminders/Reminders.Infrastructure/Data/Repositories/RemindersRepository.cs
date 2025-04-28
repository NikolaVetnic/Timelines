using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using Reminders.Application.Data.Abstractions;
using Reminders.Application.Entities.Reminders.Exceptions;

namespace Reminders.Infrastructure.Data.Repositories;

public class RemindersRepository(IRemindersDbContext dbContext) : IRemindersRepository
{
    public async Task<Reminder> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken)
    {
        return await dbContext.Reminders
                   .AsNoTracking()
                   .SingleOrDefaultAsync(r => r.Id == reminderId, cancellationToken) ??
               throw new ReminderNotFoundException(reminderId.ToString());
    }
    
    public async Task<IEnumerable<Reminder>> GetRemindersBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        return await dbContext.Reminders
            .AsNoTracking()
            .Where(r => nodeIds.Contains(r.NodeId))
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task DeleteReminder(ReminderId reminderId, CancellationToken cancellationToken)
    {
        var reminderToDelete = await dbContext.Reminders
            .FirstAsync(r => r.Id == reminderId, cancellationToken);

        dbContext.Reminders.Remove(reminderToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
