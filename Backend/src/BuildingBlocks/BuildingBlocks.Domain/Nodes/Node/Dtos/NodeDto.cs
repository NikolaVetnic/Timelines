using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

namespace BuildingBlocks.Domain.Nodes.Node.Dtos;

public class NodeDto(
    string? id,
    string title,
    string description,
    DateTime timestamp,
    int importance,
    List<string> categories,
    List<string> tags) : NodeBaseDto(id, title, description, timestamp, importance, categories, tags)
{
    public NodeDto() : this(null, string.Empty, string.Empty, default, default, [], []) { }

    [JsonPropertyName("timeline")] public required TimelineBaseDto Timeline { get; set; }

    [JsonPropertyName("fileAssets")] public List<FileAssetBaseDto> FileAssets { get; set; } = [];

    [JsonPropertyName("notes")] public List<NoteBaseDto> Notes { get; set; } = [];

    [JsonPropertyName("reminders")] public List<ReminderBaseDto> Reminders { get; set; } = [];
}
