using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.Reminders.Reminder.Dtos;

public class ReminderBaseDto(
    string? id,
    string title,
    string description,
    DateTime notifyAt,
    int priority)
{
    [JsonPropertyName("id")] public string? Id { get; } = id;

    [JsonPropertyName("title")] public string Title { get; } = title;

    [JsonPropertyName("description")] public string Description { get; } = description;

    [JsonPropertyName("notifyAt")] public DateTime NotifyAt { get; } = notifyAt;

    [JsonPropertyName("priority")] public int Priority { get; } = priority;
}
