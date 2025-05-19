using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Timelines.Phase.Dtos;

namespace BuildingBlocks.Domain.Timelines.Timeline.Dtos;

public class TimelineDto(
    string? id,
    string title,
    string description,
    string ownerId) : TimelineBaseDto(id, title, description, ownerId)
{
    public TimelineDto() : this(null, string.Empty, string.Empty, String.Empty) { }

    [JsonPropertyName("nodes")] public List<NodeBaseDto> Nodes { get; set; } = [];

    [JsonPropertyName("phases")] public List<PhaseBaseDto> Phases { get; set; } = [];
}
