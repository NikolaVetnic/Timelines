using BuildingBlocks.Domain.Enums;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.Files.File.Dtos;

public class FileAssetDto(
    string id,
    string name,
    string description,
    float size,
    EFileType type,
    string owner,
    byte[] content,
    bool isPublic,
    List<string> sharedWith,
    NodeBaseDto node) : FileAssetBaseDto(id, name, description, size, type, owner, content, isPublic, sharedWith)
{
    [JsonPropertyName("node")] public NodeBaseDto Node { get; set; } = node;
}
