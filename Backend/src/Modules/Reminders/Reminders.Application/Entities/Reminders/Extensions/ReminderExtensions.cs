namespace Reminders.Application.Entities.Reminders.Extensions;

public static class ReminderExtensions
{
    public static ReminderDto ToReminderDto(this Reminder reminder)
    {
        return new ReminderDto(
            reminder.Id.ToString(),
            reminder.Title,
            reminder.Description,
            reminder.DueDateTime,
            reminder.Priority,
            reminder.NotificationTime,
            reminder.Status);
    }

    public static IEnumerable<ReminderDto> ToReminderDtoList(this IEnumerable<Reminder> reminders)
    {
        return reminders.Select(ToReminderDto);
    }
}
