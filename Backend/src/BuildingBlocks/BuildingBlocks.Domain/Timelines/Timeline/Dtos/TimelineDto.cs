using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.Timelines.Timeline.Dtos;

public class TimelineDto(
    string? id,
    string title)
{
    [JsonPropertyName("id")] public string? Id { get; } = id;

    [JsonPropertyName("title")] public string Title { get; } = title;
}
