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
            reminder.NotifyAt,
            reminder.Priority,
            node);
    }

    public static ReminderBaseDto ToReminderBaseDto(this Reminder reminder)
    {
        return new ReminderBaseDto(
            reminder.Id.ToString(),
            reminder.Title,
            reminder.Description,
            reminder.NotifyAt,
            reminder.Priority
            );
    }
}
