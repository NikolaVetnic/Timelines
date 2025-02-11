using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.Notes.Note.Dtos;

public class NoteBaseDto(
    string? id,
    string title,
    string content, 
    DateTime timestamp,
    int importance)
{
    [JsonPropertyName("id")] public string? Id { get; } = id;

    [JsonPropertyName("title")] public string Title { get; } = title;

    [JsonPropertyName("content")] public string Content { get; } = content;

    [JsonPropertyName("timestamp")] public DateTime Timestamp { get; } = timestamp;

    [JsonPropertyName("importance")] public int Importance { get; } = importance;
}
