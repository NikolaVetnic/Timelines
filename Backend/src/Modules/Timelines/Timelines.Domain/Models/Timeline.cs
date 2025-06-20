﻿using System.Text.Json;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.Events;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Timelines.Domain.Models;

public class Timeline : Aggregate<TimelineId>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string OwnerId { get; set; }
    public List<NodeId> NodeIds { get; set; } = [];
    public List<PhaseId> PhaseIds { get; set; } = [];
    public List<PhysicalPersonId> PhysicalPersonIds { get; set; } = [];

    #region Timeline

    public static Timeline Create(TimelineId id, string title, string description, string owner)
    {
        var timeline = new Timeline
        {
            Id = id,
            Title = title,
            Description = description,
            OwnerId = owner
        };

        timeline.NodeIds = [];
        timeline.PhaseIds = [];

        timeline.AddDomainEvent(new TimelineCreatedEvent(timeline.Id));

        return timeline;
    }

    public void Update(string title)
    {
        Title = title;

        AddDomainEvent(new TimelineUpdatedEvent(Id));
    }

    #endregion

    #region Nodes

    public void AddNode(NodeId nodeId)
    {
        if (!NodeIds.Contains(nodeId))
            NodeIds.Add(nodeId);
    }

    public void RemoveNode(NodeId nodeId)
    {
        if (NodeIds.Contains(nodeId))
            NodeIds.Remove(nodeId);
    }

    #endregion

    #region Phases

    public void AddPhase(PhaseId phaseId)
    {
        if (!PhaseIds.Contains(phaseId))
            PhaseIds.Add(phaseId);
    }

    public void RemovePhase(PhaseId phaseId)
    {
        if (PhaseIds.Contains(phaseId))
            PhaseIds.Remove(phaseId);
    }

    #endregion

    #region PhysicalPersons

    public void AddPhysicalPerson(PhysicalPersonId physicalPersonId)
    {
        if (!PhysicalPersonIds.Contains(physicalPersonId))
            PhysicalPersonIds.Add(physicalPersonId);
    }

    public void RemovePhysicalPerson(PhysicalPersonId physicalPersonId)
    {
        if (PhysicalPersonIds.Contains(physicalPersonId))
            PhysicalPersonIds.Remove(physicalPersonId);
    }

    #endregion
}

public class NodeIdListConverter() : ValueConverter<List<NodeId>, string>(
    list => JsonSerializer.Serialize(list, (JsonSerializerOptions)null!),
    json => JsonSerializer.Deserialize<List<NodeId>>(json, new JsonSerializerOptions()) ?? new List<NodeId>());

public class PhaseIdListConverter() : ValueConverter<List<PhaseId>, string>(
    list => JsonSerializer.Serialize(list, (JsonSerializerOptions)null!),
    json => JsonSerializer.Deserialize<List<PhaseId>>(json, new JsonSerializerOptions()) ?? new List<PhaseId>());
