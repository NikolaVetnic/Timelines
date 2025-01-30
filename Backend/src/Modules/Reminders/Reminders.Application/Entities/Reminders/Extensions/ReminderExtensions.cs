using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;

namespace Reminders.Application.Entities.Reminders.Extensions;

public static class ReminderExtensions
{
    public static ReminderDto ToReminderDto(this Reminder reminder, NodeBaseDto node)
    {
        return new ReminderDto(
            reminder.Id.ToString(),
            reminder.Title,
            reminder.Description,
            reminder.DueDateTime,
            reminder.Priority,
            reminder.NotificationTime,
            reminder.Status,
            node);
    }
}
