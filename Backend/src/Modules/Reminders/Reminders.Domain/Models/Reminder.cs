using Reminders.Domain.Events;

namespace Reminders.Domain.Models;

public class Reminder : Aggregate<ReminderId>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime DueDateTime { get; set; }
    public required int Priority { get; set; }
    public required DateTime NotificationTime { get; set; }
    public required string Status { get; set; }

    #region Reminder

    public static Reminder Create(ReminderId id, string title, string description, DateTime dueDateTime, int priority, DateTime notificationTime, string status)
    {
        var reminder = new Reminder
        {
            Id = id,
            Title = title,
            Description = description,
            DueDateTime = dueDateTime,
            Priority = priority,
            NotificationTime = notificationTime,
            Status = status
        };

        reminder.AddDomainEvent(new ReminderCreatedEvent(reminder));

        return reminder;
    }

    public void Update(string title, string description, DateTime dueDateTime, int priority, DateTime notificationTime, string status)
    {
        Title = title;
        Description = description;
        DueDateTime = dueDateTime;
        Priority = priority;
        NotificationTime = notificationTime;
        Status = status;

        AddDomainEvent(new ReminderUpdatedEvent(this));
    }

    #endregion
}
