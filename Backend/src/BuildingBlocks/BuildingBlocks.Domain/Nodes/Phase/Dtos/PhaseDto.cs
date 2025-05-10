using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Nodes.Node.Dtos;

namespace BuildingBlocks.Domain.Nodes.Phase.Dtos;

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
List<string> nodeIds) : PhaseBaseDto(id, title, description, startDate, endDate, duration, status, progress, isCompleted, parent, dependsOn, assignedTo, stakeholders, tags, nodeIds)
{
    [JsonPropertyName("nodes")] public List<NodeBaseDto> Nodes { get; set; } = [];

}