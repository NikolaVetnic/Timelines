using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Nodes.Domain.Models;

public class Phase : Entity<PhaseId>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime? EndDate { get; set; }
    public required TimeSpan? Duration { get; set; }
    public required string Status { get; set; }
    public required decimal Progress { get; set; }
    public required bool IsCompleted { get; set; }
    public required PhaseId Parent { get; set; }
    public required PhaseId[] DependsOn { get; set; }
    public required string AssignedTo { get; set; }
    public required string[] Stakeholders { get; set; }
    public required string[] Tags { get; set; }
    public List<NodeId> NodeIds { get; set; } = [];

    public static Phase Create(PhaseId id, string title, string description, DateTime startDate, DateTime endDate, TimeSpan duration, string status, decimal progress,
        bool isCompleted, PhaseId parent, PhaseId[] dependsOn, string assignedTo, string[] stakeholders, string[] tags)
    {
        var phase = new Phase
        {
            Id = id,
            Title = title,
            Description = description,
            StartDate = startDate,
            EndDate = endDate,
            Duration = duration,
            Status = status,
            Progress = progress,
            IsCompleted = isCompleted,
            Parent = parent,
            DependsOn = dependsOn,
            AssignedTo = assignedTo,
            Stakeholders = stakeholders,
            Tags = tags
        };

        return phase;
    }
}

public class NodeIdListConverter() : ValueConverter<List<NodeId>, string>(
    list => JsonSerializer.Serialize(list, (JsonSerializerOptions)null!),
    json => JsonSerializer.Deserialize<List<NodeId>>(json, new JsonSerializerOptions()) ?? new List<NodeId>());
