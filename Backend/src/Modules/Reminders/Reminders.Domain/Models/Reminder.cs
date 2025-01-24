using Reminders.Domain.Events;

namespace Reminders.Domain.Models;

public class Reminder : Aggregate<ReminderId>
{
    private readonly List<Reminder> _relatedReminder = [];

    public IReadOnlyList<Reminder> RelatedReminders => _relatedReminder.AsReadOnly();

    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime NotifyAt { get; set; }
    public required int Priority { get; set; }

    #region Reminder

    public static Reminder Create(ReminderId id, string title, string description, DateTime notifyAt, int priority, List<Reminder> relatedReminders)
    {
        var reminder = new Reminder
        {
            Id = id,
            Title = title,
            Description = description,
            NotifyAt = notifyAt,
            Priority = priority,
        };

        foreach (var relatedReminder in relatedReminders)
            reminder.AddRelatedReminder(relatedReminder);

        reminder.AddDomainEvent(new ReminderCreatedEvent(reminder));

        return reminder;
    }

    public void Update(string title, string description, DateTime notifyAt, int priority, DateTime notificationTime, string status)
    {
        Title = title;
        Description = description;
        NotifyAt = notifyAt;
        Priority = priority;

        AddDomainEvent(new ReminderUpdatedEvent(this));
    }

    #endregion

    #region RelatedReminders

    private void AddRelatedReminder(Reminder reminder)
    {
        _relatedReminder.Add(reminder);
    }

    private void RemoveReminder(Reminder reminder)
    {
        _relatedReminder.Remove(reminder);
    }

    #endregion
}
