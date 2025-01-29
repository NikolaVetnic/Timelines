using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Nodes.Node.Dtos;

namespace BuildingBlocks.Domain.Reminders.Dtos;

public class ReminderDto(
    string? id,
    string title,
    string description,
    DateTime dueDateTime,
    int priority,
    DateTime notificationTime,
    string status,
    NodeBaseDto node) // todo: Refactor this so that it extends ReminderBaseDto
{
    [JsonPropertyName("id")] public string? Id { get; } = id;

    [JsonPropertyName("title")] public string Title { get; } = title;

    [JsonPropertyName("description")] public string Description { get; } = description;

    [JsonPropertyName("dueDateTime")] public DateTime DueDateTime { get; } = dueDateTime;

    [JsonPropertyName("priority")] public int Priority { get; } = priority;

    [JsonPropertyName("notificationTime")] public DateTime NotificationTime { get; } = notificationTime;

    [JsonPropertyName("status")] public string Status { get; } = status;
    
    [JsonPropertyName("node")] public NodeBaseDto Node { get; set; } = node;
}
