using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.Nodes.Node.Dtos;

public class NodeBaseDto(
    string? id,
    string title,
    string description,
    DateTime timestamp,
    int importance,
    string phase,
    List<string> categories,
    List<string> tags) // todo: Refactor this so that it extends NodeBaseDto
{
    [JsonPropertyName("id")] public string? Id { get; } = id;

    [JsonPropertyName("title")] public string Title { get; } = title;

    [JsonPropertyName("description")] public string Description { get; } = description;

    [JsonPropertyName("timestamp")] public DateTime Timestamp { get; } = timestamp;

    [JsonPropertyName("importance")] public int Importance { get; } = importance;

    [JsonPropertyName("phase")] public string Phase { get; } = phase;

    [JsonPropertyName("categories")] public List<string> Categories { get; } = categories;

    [JsonPropertyName("tags")] public List<string> Tags { get; } = tags;
}
