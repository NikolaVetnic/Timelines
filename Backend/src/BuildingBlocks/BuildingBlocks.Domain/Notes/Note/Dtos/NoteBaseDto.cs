using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace BuildingBlocks.Domain.Notes.Note.Dtos;

public class NoteBaseDto(
    string? id,
    string title,
    string content,
    DateTime timestamp,
    string owner,
    List<NoteId> relatedNotes,
    List<string> sharedWith,
    bool isPublic,
    DateTime? createdAt,
    DateTime? lastModifiedAt)
{
    [JsonPropertyName("id")] public string? Id { get; } = id;

    [JsonPropertyName("title")] public string Title { get; } = title;

    [JsonPropertyName("content")] public string Content { get; } = content;

    [JsonPropertyName("timestamp")] public DateTime Timestamp { get; } = timestamp;

    [JsonPropertyName("owner")] public string Owner { get; } = owner;

    [JsonPropertyName("relatedNotes")] public List<NoteId> RelatedNotes { get; } = relatedNotes;

    [JsonPropertyName("sharedWith")] public List<string> SharedWith { get; } = sharedWith;

    [JsonPropertyName("isPublic")] public bool IsPublic { get; } = isPublic;

    [JsonPropertyName("createdAt")] public DateTime? CreatedAt { get; } = createdAt;

    [JsonPropertyName("lastModifiedAt")] public DateTime? LastModifiedAt { get; } = lastModifiedAt;
}
