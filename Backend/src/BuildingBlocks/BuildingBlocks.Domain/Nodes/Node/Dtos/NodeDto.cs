using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Nodes.Phase.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;

namespace BuildingBlocks.Domain.Nodes.Node.Dtos;

public class NodeDto(
    string? id,
    string title,
    string description,
    DateTime timestamp,
    int importance,
    List<string> categories,
    List<string> tags,
    PhaseBaseDto phase) : NodeBaseDto(id, title, description, timestamp, importance, categories, tags)
{
    [JsonPropertyName("reminders")] public List<ReminderBaseDto> Reminders { get; } = [];

    [JsonPropertyName("phase")] public PhaseBaseDto Phase { get; set; } = phase;
}
