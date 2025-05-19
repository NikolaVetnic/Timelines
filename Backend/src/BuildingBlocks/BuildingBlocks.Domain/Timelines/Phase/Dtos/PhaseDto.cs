using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

namespace BuildingBlocks.Domain.Timelines.Phase.Dtos;

public class PhaseDto(
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
List<string> tags,
TimelineBaseDto timeline) : PhaseBaseDto(id, title, description, startDate, endDate, duration, status, progress, isCompleted, parent, dependsOn, assignedTo, stakeholders, tags)
{
    [JsonPropertyName("timeline")] public TimelineBaseDto Timeline { get; set; } = timeline;
}
