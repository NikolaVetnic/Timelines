using System.Text.Json;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.Events;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Reminders.Domain.Models;

public class Reminder : Aggregate<ReminderId>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime NotifyAt { get; set; }
    public required int Priority { get; set; }
    public List<ReminderId> RelatedReminders { get; set; } = [];
    public required NodeId NodeId { get; set; }

    #region Reminder

    public static Reminder Create(ReminderId id, string title, string description, DateTime notifyAt, int priority , NodeId nodeId)
    {
        var reminder = new Reminder
        {
            Id = id,
            Title = title,
            Description = description,
            NotifyAt = notifyAt,
            Priority = priority,
            NodeId = nodeId
        };

        reminder.RelatedReminders = [];

        reminder.AddDomainEvent(new ReminderCreatedEvent(reminder.Id));

        return reminder;
    }

    public void Update(string title, string description, DateTime notifyAt, int priority)
    {
        Title = title;
        Description = description;
        NotifyAt = notifyAt;
        Priority = priority;

        AddDomainEvent(new ReminderUpdatedEvent(Id));
    }

    #endregion

    #region RelatedReminders
    public void AddRelatedReminders(ReminderId reminderId)
    {
        if (!RelatedReminders.Contains(reminderId))
            RelatedReminders.Add(reminderId);
    }

    public void RemoveRelatedReminders(ReminderId reminderId)
    {
        if (RelatedReminders.Contains(reminderId))
            RelatedReminders.Remove(reminderId);
    }

    #endregion
}

public class RelatedReminderIdListConverter() : ValueConverter<List<ReminderId>, string>(
    list => JsonSerializer.Serialize(list, (JsonSerializerOptions)null!),
    json => JsonSerializer.Deserialize<List<ReminderId>>(json, new JsonSerializerOptions()) ?? new List<ReminderId>());
