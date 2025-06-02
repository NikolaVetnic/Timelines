using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface IRemindersService
{
    Task<List<ReminderDto>> ListRemindersPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<ReminderDto>> ListFlaggedForDeletionRemindersPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<ReminderBaseDto>> ListRemindersByNodeIdPaginated(NodeId nodeId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<ReminderDto> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken);
    Task<ReminderBaseDto> GetReminderBaseByIdAsync(ReminderId reminderId, CancellationToken cancellationToken);
    Task<List<ReminderBaseDto>> GetRemindersBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
    Task<long> CountRemindersAsync(CancellationToken cancellationToken);
    Task<long> CountRemindersByNodeIdAsync(NodeId nodeId, CancellationToken cancellationToken);

    Task DeleteReminder(ReminderId reminderId, CancellationToken cancellationToken);
    Task DeleteReminders(NodeId nodeId, IEnumerable<ReminderId> reminderIds, CancellationToken cancellationToken);
    Task DeleteRemindersByNodeIds(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
}
