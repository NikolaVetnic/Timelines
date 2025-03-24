using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.Timelines.Timeline.Dtos;

public class TimelineBaseDto(
    string? id,
    string title,
    string description)
{
    [JsonPropertyName("id")] public string? Id { get; } = id;

    [JsonPropertyName("title")] public string Title { get; } = title;

    [JsonPropertyName("description")] public string Description { get; } = description;
}
