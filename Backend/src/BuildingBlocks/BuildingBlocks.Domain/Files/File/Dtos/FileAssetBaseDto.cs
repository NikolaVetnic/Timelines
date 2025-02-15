using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.Files.File.Dtos;

public class FileAssetBaseDto(
    string id,
    string name,
    float size,
    string type,
    string owner,
    string description, 
    List<string> sharedWith)
{
    [JsonPropertyName("id")] public string? Id { get; } = id;

    [JsonPropertyName("name")] public string Name { get; } = name;

    [JsonPropertyName("size")] public float Size { get; } = size;

    [JsonPropertyName("type")] public string Type { get; } = type;

    [JsonPropertyName("owner")] public string Owner { get; } = owner;

    [JsonPropertyName("description")] public string Description { get; } = description;

    [JsonPropertyName("sharedWith")] public List<string> SharedWith { get; } = sharedWith;
}
