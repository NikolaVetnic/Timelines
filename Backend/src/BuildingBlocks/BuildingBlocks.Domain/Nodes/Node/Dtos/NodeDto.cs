using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

namespace BuildingBlocks.Domain.Nodes.Node.Dtos;

public class NodeDto(
    string? id,
    string title,
    string description,
    DateTime timestamp,
    int importance,
    string phase,
    List<string> categories,
    List<string> tags,
    TimelineBaseDto timeline) : NodeBaseDto(id, title, description, timestamp, importance, phase, categories, tags)
{
    [JsonPropertyName("reminders")] public List<ReminderBaseDto> Reminders { get; } = [];
    [JsonPropertyName("timelines")] public TimelineBaseDto Timelines { get; } = timeline;
}
