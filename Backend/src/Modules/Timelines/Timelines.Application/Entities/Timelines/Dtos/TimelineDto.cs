using System.Text.Json.Serialization;

namespace Timelines.Application.Entities.Timelines.Dtos;

public class TimelineDto(
    string id,
    string title)
{
    [JsonPropertyName("id")] public string? Id { get; } = id;

    [JsonPropertyName("title")] public string Title { get; } = title;
}
