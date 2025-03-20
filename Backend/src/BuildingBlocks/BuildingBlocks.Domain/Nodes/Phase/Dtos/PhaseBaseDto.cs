using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.Nodes.Phase.Dtos;

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
    string parent,
    List<string> dependsOn,
    string assignedTo,
    List<string> stakeholders,
    List<string> tags)
{
    [JsonPropertyName("id")] public string? Id { get; } = id;
    [JsonPropertyName("title")] public string Title { get; } = title;
    [JsonPropertyName("description")] public string Description { get; } = description;
    [JsonPropertyName("startDate")] public DateTime StartDate { get; } = startDate;
    [JsonPropertyName("endDate")] public DateTime? EndDate { get; } = endDate;
    [JsonPropertyName("duration")] public TimeSpan? Duration { get; } = duration;
    [JsonPropertyName("status")] public string Status { get; } = status;
    [JsonPropertyName("progress")] public decimal Progress { get; } = progress;
    [JsonPropertyName("isCompleted")] public bool IsCompleted { get; } = isCompleted;
    [JsonPropertyName("parent")] public string Parent { get; } = parent;
    [JsonPropertyName("dependsOn")] public List<string> DependsOn { get; } = dependsOn;
    [JsonPropertyName("assignedTo")] public string AssignedTo { get; } = assignedTo;
    [JsonPropertyName("stakeholders")] public List<string> Stakeholders { get; } = stakeholders;
    [JsonPropertyName("tags")] public List<string> Tags { get; } = tags;
}