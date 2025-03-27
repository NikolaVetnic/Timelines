using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;

namespace BuildingBlocks.Domain.Nodes.Node.Dtos;

public class NodeDto(
    string? id,
    string title,
    string description,
    DateTime timestamp,
    int importance,
    string phase,
    List<string> categories,
    List<string> tags)
    : NodeBaseDto(id, title, description, timestamp, importance, phase, categories, tags)
{
    public NodeDto() : this(null, string.Empty, string.Empty, default, default, string.Empty, [], []) { }

    [JsonPropertyName("reminders")] public List<ReminderBaseDto> Reminders { get; set; } = [];

    [JsonPropertyName("notes")] public List<NoteBaseDto> Notes { get; } = [];
}
