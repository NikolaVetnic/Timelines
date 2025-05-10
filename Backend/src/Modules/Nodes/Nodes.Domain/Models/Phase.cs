using System.Text.Json;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Nodes.Phase.Events;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Nodes.Domain.Models;

public class Phase : Aggregate<PhaseId>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime? EndDate { get; set; }
    public required TimeSpan? Duration { get; set; }
    public required string Status { get; set; }
    public required decimal Progress { get; set; }
    public required bool IsCompleted { get; set; }
    public required PhaseId? Parent { get; set; }
    public required List<PhaseId> DependsOn { get; set; }
    public required string AssignedTo { get; set; }
    public List<string> Stakeholders { get; set; } = [];
    public List<string> Tags { get; set; } = [];
    public List<NodeId> NodeIds { get; set; } = [];

    #region Phase

    public static Phase Create(PhaseId id, string title, string description,
        DateTime startDate, DateTime? endDate, TimeSpan? duration,
        string status, decimal progress, bool isCompleted,
        PhaseId parent, List<PhaseId> dependsOn, string assignedTo,
        List<string> stakeholders, List<string> tags)
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
            AssignedTo = assignedTo
        };

        foreach (var stakeholder in stakeholders)
            phase.AddStakeholder(stakeholder);

        foreach (var tag in tags)
            phase.AddTag(tag);

        phase.NodeIds = [];

        phase.AddDomainEvent(new PhaseCreatedEvent(phase.Id));

        return phase;
    }

    public void Update(PhaseId id, string title, string description,
        DateTime startDate, DateTime? endDate, TimeSpan? duration,
        string status, decimal progress, bool isCompleted,
        PhaseId parent, List<PhaseId> dependsOn, string assignedTo)
    {
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Duration = duration;
        Status = status;
        Progress = progress;
        IsCompleted = isCompleted;
        Parent = parent;
        DependsOn = dependsOn;
        AssignedTo = assignedTo;

        AddDomainEvent(new PhaseUpdatedEvent(Id));
    }

    #endregion

    #region Stakeholders

    private void AddStakeholder(string stakeholder)
    {
        Stakeholders.Remove(stakeholder);
    }

    private void RemoveStakeholder(string stakeholder)
    {
        Stakeholders.Remove(stakeholder);
    }

    #endregion

    #region Tags

    private void AddTag(string tag)
    {
        Tags.Remove(tag);
    }
    private void RemoveTag(string tag)
    {
        Tags.Remove(tag);
    }

    #endregion

    #region Nodes

    public void AddNodes(NodeId nodeId)
    {
        if (!NodeIds.Contains(nodeId))
            NodeIds.Add(nodeId);
    }

    public void RemoveNodes(NodeId nodeId)
    {
        if (NodeIds.Contains(nodeId))
            NodeIds.Remove(nodeId);
    }

    #endregion
}

public class DependsOnPhaseIdListConverter() : ValueConverter<List<PhaseId>, string>(
    list => JsonSerializer.Serialize(list, (JsonSerializerOptions)null!),
    json => JsonSerializer.Deserialize<List<PhaseId>>(json, new JsonSerializerOptions()) ?? new List<PhaseId>());

public class NodeIdListConverter() : ValueConverter<List<NodeId>, string>(
    list => JsonSerializer.Serialize(list, (JsonSerializerOptions)null!),
    json => JsonSerializer.Deserialize<List<NodeId>>(json, new JsonSerializerOptions()) ?? new List<NodeId>());