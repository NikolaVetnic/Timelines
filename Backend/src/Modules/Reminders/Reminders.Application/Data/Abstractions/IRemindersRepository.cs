using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

namespace Reminders.Application.Data.Abstractions;

public interface IRemindersRepository
{
    Task AddReminderAsync(Reminder reminder, CancellationToken cancellationToken);
    
    Task<List<Reminder>> ListRemindersPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<Reminder>> ListRemindersByNodeIdPaginatedAsync(NodeId nodeId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<Reminder> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken);
    Task<long> CountAllRemindersAsync(CancellationToken cancellationToken);
    Task<long> CountAllRemindersByNodeIdAsync(NodeId nodeId, CancellationToken cancellationToken);

    Task<IEnumerable<Reminder>> GetRemindersBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);

    Task UpdateReminderAsync(Reminder reminder, CancellationToken cancellationToken);
    Task DeleteReminder(ReminderId reminderId, CancellationToken cancellationToken);
    Task DeleteReminders(IEnumerable<ReminderId> reminderIds, CancellationToken cancellationToken);
    Task DeleteRemindersByNodeIds(IEnumerable<NodeId> reminderIds, CancellationToken cancellationToken);
}
