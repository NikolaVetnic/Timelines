﻿using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using BuildingBlocks.Domain.Timelines.Phase.Events;

namespace Timelines.Domain.Models;

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
    public required List<PhaseId> DependsOn { get; set; }
    public required string AssignedTo { get; set; }
    public List<string> Stakeholders { get; set; } = [];
    public List<string> Tags { get; set; } = [];
    public required TimelineId TimelineId { get; set; }
    public required string OwnerId { get; set; }

    #region Phase

    public static Phase Create(PhaseId id, string title, string description,
        DateTime startDate, DateTime? endDate, TimeSpan? duration,
        string status, decimal progress, bool isCompleted,
        List<PhaseId> dependsOn, string assignedTo,
        List<string> stakeholders, List<string> tags, TimelineId timelineId, string ownerId)
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
            DependsOn = dependsOn,
            AssignedTo = assignedTo,
            TimelineId = timelineId,
            OwnerId = ownerId
        };

        foreach (var stakeholder in stakeholders)
            phase.AddStakeholder(stakeholder);

        foreach (var tag in tags)
            phase.AddTag(tag);

        phase.AddDomainEvent(new PhaseCreatedEvent(phase.Id));

        return phase;
    }

    public void Update(PhaseId id, string title, string description,
        DateTime startDate, DateTime? endDate, TimeSpan? duration,
        string status, decimal progress, bool isCompleted,
        List<PhaseId> dependsOn, string assignedTo)
    {
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Duration = duration;
        Status = status;
        Progress = progress;
        IsCompleted = isCompleted;
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
}

public class DependsOnPhaseIdListConverter() : ValueConverter<List<PhaseId>, string>(
    list => JsonSerializer.Serialize(list, (JsonSerializerOptions)null!),
    json => JsonSerializer.Deserialize<List<PhaseId>>(json, new JsonSerializerOptions()) ?? new List<PhaseId>());
