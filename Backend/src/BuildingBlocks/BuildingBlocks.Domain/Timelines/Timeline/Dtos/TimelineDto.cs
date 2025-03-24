using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Nodes.Node.Dtos;

namespace BuildingBlocks.Domain.Timelines.Timeline.Dtos;

public class TimelineDto(
    string? id,
    string title,
    string description) : TimelineBaseDto(id, title, description)
{
    [JsonPropertyName("nodes")] public List<NodeBaseDto> Nodes { get; } = [];
}
