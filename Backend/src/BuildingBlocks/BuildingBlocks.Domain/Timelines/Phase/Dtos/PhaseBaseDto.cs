using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;

namespace BuildingBlocks.Domain.Timelines.Phase.Dtos;

public class PhaseBaseDto(
    string? id,
    string title,
    string description,
    DateTime startDate,
    DateTime? endDate,
    TimeSpan? duration,
    string status,
    decimal progress,
    bool isCompleted,
    PhaseId parent,
    List<PhaseId> dependsOn,
    string assignedTo,
    List<string> stakeholders,
    List<string> tags
)
{
    [JsonPropertyName("id")] public string? Id { get; set; } = id;

    [JsonPropertyName("title")] public string Title { get; set; } = title;

    [JsonPropertyName("description")] public string Description { get; set; } = description;

    [JsonPropertyName("startDate")] public DateTime StartDate { get; set; } = startDate;

    [JsonPropertyName("endDate")] public DateTime? EndDate { get; set; } = endDate;

    [JsonPropertyName("duration")] public TimeSpan? Duration { get; set; } = duration;

    [JsonPropertyName("status")] public string Status { get; set; } = status;

    [JsonPropertyName("progress")] public decimal Progress { get; set; } = progress;

    [JsonPropertyName("isCompleted")] public bool IsCompleted { get; set; } = isCompleted;

    [JsonPropertyName("parent")] public PhaseId Parent { get; set; } = parent;

    [JsonPropertyName("dependsOn")] public List<PhaseId> DependsOn { get; set; } = dependsOn;

    [JsonPropertyName("assignedTo")] public string AssignedTo { get; set; } = assignedTo;

    [JsonPropertyName("stakeholders")] public List<string> Stakeholders { get; set; } = stakeholders;

    [JsonPropertyName("tags")] public List<string> Tags { get; set; } = tags;
}
