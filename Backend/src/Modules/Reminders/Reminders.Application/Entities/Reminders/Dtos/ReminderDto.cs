using System.Text.Json.Serialization;

namespace Reminders.Application.Entities.Reminders.Dtos;

public class ReminderDto(
    string? id,
    string title,
    string description,
    DateTime notifyAt,
    int priority,
    List<Reminder> relatedReminders)
{
    [JsonPropertyName("id")] public string? Id { get; } = id;

    [JsonPropertyName("title")] public string Title { get; } = title;

    [JsonPropertyName("description")] public string Description { get; } = description;

    [JsonPropertyName("notifyAt")] public DateTime NotifyAt { get; } = notifyAt;

    [JsonPropertyName("priority")] public int Priority { get; } = priority;

    [JsonPropertyName("relatedReminders")] public List<Reminder> RelatedReminders { get; } = relatedReminders;

}
