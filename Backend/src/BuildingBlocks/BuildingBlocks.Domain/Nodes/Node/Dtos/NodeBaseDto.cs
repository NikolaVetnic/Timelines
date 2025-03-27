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
    List<string> tags)
{
    [JsonPropertyName("id")] public string? Id { get; set; } = id;

    [JsonPropertyName("title")] public string Title { get; set; } = title;

    [JsonPropertyName("description")] public string Description { get; set; } = description;

    [JsonPropertyName("timestamp")] public DateTime Timestamp { get; set; } = timestamp;

    [JsonPropertyName("importance")] public int Importance { get; set; } = importance;

    [JsonPropertyName("phase")] public string Phase { get; set; } = phase;

    [JsonPropertyName("categories")] public List<string> Categories { get; set; } = categories;

    [JsonPropertyName("tags")] public List<string> Tags { get; set; } = tags;
}
