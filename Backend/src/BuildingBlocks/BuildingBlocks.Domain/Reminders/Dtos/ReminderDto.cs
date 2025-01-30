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
    NodeBaseDto node) : ReminderBaseDto(id, title, description, dueDateTime, priority, notificationTime, status)
{
    [JsonPropertyName("node")] public NodeBaseDto Node { get; set; } = node;
}
