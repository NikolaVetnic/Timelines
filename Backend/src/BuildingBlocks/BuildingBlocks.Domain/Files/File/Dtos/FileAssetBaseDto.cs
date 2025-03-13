using BuildingBlocks.Domain.Enums;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.Files.File.Dtos;

public class FileAssetBaseDto(
    string id,
    string name,
    string description,
    float size,
    EFileType type,
    string owner,
    byte[] content,
    bool isPublic,
    List<string> sharedWith)
{
    [JsonPropertyName("id")] public string? Id { get; } = id;

    [JsonPropertyName("name")] public string Name { get; } = name;

    [JsonPropertyName("description")] public string Description { get; } = description;

    [JsonPropertyName("size")] public float Size { get; } = size;

    [JsonPropertyName("type")] public EFileType Type { get; } = type;

    [JsonPropertyName("owner")] public string Owner { get; } = owner;

    [JsonPropertyName("content")] public byte[] Content { get; } = content;

    [JsonPropertyName("type")] public bool IsPublic { get; } = isPublic;

    [JsonPropertyName("sharedWith")] public List<string> SharedWith { get; } = sharedWith;
}
