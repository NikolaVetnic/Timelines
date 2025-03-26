using System.Text.Json;
using BuildingBlocks.Domain.Nodes.Node.Events;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Nodes.Domain.Models;

// ReSharper disable NullableWarningSuppressionIsUsed

public class Node : Aggregate<NodeId>
{
    public List<string> Categories { get; set; } = [];
    public List<string> Tags { get; set; } = [];

    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Phase { get; set; }
    public required DateTime Timestamp { get; set; }
    public required int Importance { get; set; }

    public List<ReminderId> ReminderIds { get; set; } = [];
    public required TimelineId TimelineId { get; set; }

    #region Node

    public static Node Create(NodeId id, string title, string description, string phase,
        DateTime timestamp, int importance, List<string> categories, List<string> tags, TimelineId timelineId)
    {
        var node = new Node
        {
            Id = id,
            Title = title,
            Description = description,
            Phase = phase,
            Timestamp = timestamp,
            Importance = importance,
            TimelineId = timelineId
        };

        foreach (var category in categories)
            node.AddCategory(category);

        foreach (var tag in tags)
            node.AddTag(tag);

        node.ReminderIds = [];

        node.AddDomainEvent(new NodeCreatedEvent(node.Id));

        return node;
    }

    public void Update(string title, string description, DateTime timestamp,
        int importance, string phase)
    {
        Title = title;
        Description = description;
        Timestamp = timestamp;
        Importance = importance;
        Phase = phase;

        AddDomainEvent(new NodeUpdatedEvent(Id));
    }

    #endregion

    #region Reminders

    public void AddReminder(ReminderId reminderId)
    {
        if (!ReminderIds.Contains(reminderId))
            ReminderIds.Add(reminderId);
    }

    public void RemoveReminder(ReminderId reminderId)
    {
        if (ReminderIds.Contains(reminderId))
            ReminderIds.Remove(reminderId);
    }

    #endregion

    #region Categories

    private void AddCategory(string category)
    {
        Categories.Add(category);
    }

    private void RemoveCategory(string category)
    {
        Categories.Remove(category);
    }

    #endregion

    #region Tags

    private void AddTag(string tag)
    {
        Tags.Add(tag);
    }

    private void RemoveTag(string tag)
    {
        Tags.Remove(tag);
    }

    #endregion
}

public class ReminderIdListConverter() : ValueConverter<List<ReminderId>, string>(
    list => JsonSerializer.Serialize(list, (JsonSerializerOptions)null!),
    json => JsonSerializer.Deserialize<List<ReminderId>>(json, new JsonSerializerOptions()) ?? new List<ReminderId>());
