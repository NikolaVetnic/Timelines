using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.Reminders.Dtos;

public class ReminderBaseDto(
    string? id,
    string title,
    string description,
    DateTime dueDateTime,
    int priority,
    DateTime notificationTime,
    string status)
{
    [JsonPropertyName("id")] public string? Id { get; } = id;

    [JsonPropertyName("title")] public string Title { get; } = title;

    [JsonPropertyName("description")] public string Description { get; } = description;

    [JsonPropertyName("dueDateTime")] public DateTime DueDateTime { get; } = dueDateTime;

    [JsonPropertyName("priority")] public int Priority { get; } = priority;

    [JsonPropertyName("notificationTime")] public DateTime NotificationTime { get; } = notificationTime;

    [JsonPropertyName("status")] public string Status { get; } = status;
}
