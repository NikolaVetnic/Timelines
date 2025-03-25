using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface IRemindersService
{
    Task<ReminderDto> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken);
    Task<ReminderBaseDto> GetReminderBaseByIdAsync(ReminderId reminderId, CancellationToken cancellationToken);
    Task<List<ReminderBaseDto>> GetRemindersBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
}
