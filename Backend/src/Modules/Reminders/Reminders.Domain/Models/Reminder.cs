using BuildingBlocks.Domain.ValueObjects.Ids;
using Reminders.Domain.Events;

namespace Reminders.Domain.Models;

public class Reminder : Aggregate<ReminderId>
{
    public required string Title { get; set; }

    #region Reminder

    public static Reminder Create(ReminderId id, string title)
    {
        var reminder = new Reminder
        {
            Id = id,
            Title = title
        };

        reminder.AddDomainEvent(new ReminderCreatedEvent(reminder));

        return reminder;
    }

    public void Update(string title)
    {
        Title = title;

        AddDomainEvent(new ReminderUpdatedEvent(this));
    }

    #endregion
}
