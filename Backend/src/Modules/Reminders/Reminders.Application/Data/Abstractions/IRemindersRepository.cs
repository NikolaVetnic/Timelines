using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

namespace Reminders.Application.Data.Abstractions;

public interface IRemindersRepository
{
    Task<Reminder> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken);

    Task<IEnumerable<Reminder>> GetRemindersBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);

    Task DeleteReminder(ReminderId reminderId, CancellationToken cancellationToken);
}

