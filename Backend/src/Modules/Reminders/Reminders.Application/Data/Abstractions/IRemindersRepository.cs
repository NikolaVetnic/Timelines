using System.Linq.Expressions;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

namespace Reminders.Application.Data.Abstractions;

public interface IRemindersRepository
{
    Task AddReminderAsync(Reminder reminder, CancellationToken cancellationToken);

    Task<List<Reminder>> ListRemindersPaginatedAsync(Expression<Func<Reminder, bool>> predicate, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<Reminder> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken);
    Task<Reminder> GetFlaggedForDeletionReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken);
    Task<long> CountRemindersAsync(Expression<Func<Reminder, bool>> predicate, CancellationToken cancellationToken);

    Task<IEnumerable<Reminder>> GetRemindersBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);

    Task UpdateReminderAsync(Reminder reminder, CancellationToken cancellationToken);
    Task DeleteReminder(ReminderId reminderId, CancellationToken cancellationToken);
    Task DeleteReminders(IEnumerable<ReminderId> reminderIds, CancellationToken cancellationToken);
    Task DeleteRemindersByNodeIds(IEnumerable<NodeId> reminderIds, CancellationToken cancellationToken);

    Task ReviveReminder(ReminderId reminderId, CancellationToken cancellationToken);
}
