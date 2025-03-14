using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Nodes.Node.Dtos;

namespace BuildingBlocks.Domain.Timelines.Timeline.Dtos;

public class TimelineDto(
    string? id,
    string title) : TimelineBaseDto(id, title)
{
    [JsonPropertyName("nodes")] public List<NodeBaseDto> Nodes { get; } = [];
}
