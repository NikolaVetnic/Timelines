using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.Timelines.Timeline.Dtos;

public class TimelineBaseDto(
    string? id,
    string title,
    string description,
    string ownerId)
{
    [JsonPropertyName("id")] public string? Id { get; set; } = id;

    [JsonPropertyName("title")] public string Title { get; set; } = title;

    [JsonPropertyName("description")] public string Description { get; set; } = description;
    [JsonPropertyName("owner")] public string OwnerId { get; set; } = ownerId;
}
