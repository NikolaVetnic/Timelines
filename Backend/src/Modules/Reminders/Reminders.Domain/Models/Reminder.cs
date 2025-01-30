using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.Events;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

namespace Reminders.Domain.Models;

public class Reminder : Aggregate<ReminderId>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime DueDateTime { get; set; }
    public required int Priority { get; set; }
    public required DateTime NotificationTime { get; set; }
    public required string Status { get; set; }
    public required NodeId NodeId { get; set; }

    #region Reminder

    public static Reminder Create(ReminderId id, string title, string description, DateTime dueDateTime, int priority, DateTime notificationTime, string status, NodeId nodeId)
    {
        var reminder = new Reminder
        {
            Id = id,
            Title = title,
            Description = description,
            DueDateTime = dueDateTime,
            Priority = priority,
            NotificationTime = notificationTime,
            Status = status,
            NodeId = nodeId
        };

        reminder.AddDomainEvent(new ReminderCreatedEvent(reminder.Id));

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

        AddDomainEvent(new ReminderUpdatedEvent(Id));
    }

    #endregion
}
