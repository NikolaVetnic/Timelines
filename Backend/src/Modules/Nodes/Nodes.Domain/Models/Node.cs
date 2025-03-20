using System.Text.Json;
using BuildingBlocks.Domain.Nodes.Node.Events;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Nodes.Domain.Models;

// ReSharper disable NullableWarningSuppressionIsUsed

public class Node : Aggregate<NodeId>
{
    private readonly List<string> _categories = new();
    private readonly List<string> _tags = new();

    public IReadOnlyList<string> Categories => _categories.AsReadOnly();
    public IReadOnlyList<string> Tags => _tags.AsReadOnly();

    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime Timestamp { get; set; }
    public required int Importance { get; set; }
    public required PhaseId PhaseId { get; set; }
    public List<ReminderId> ReminderIds { get; set; } = [];

    #region Node

    public static Node Create(NodeId id, string title, string description, DateTime timestamp, int importance, List<string> categories, List<string> tags, PhaseId phaseId)
    {
        var node = new Node
        {
            Id = id,
            Title = title,
            Description = description,
            Timestamp = timestamp,
            Importance = importance,
            PhaseId = phaseId
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
        int importance, PhaseId phaseId)
    {
        Title = title;
        Description = description;
        Timestamp = timestamp;
        Importance = importance;
        PhaseId = phaseId;

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
        _categories.Add(category);
    }

    private void RemoveCategory(string category)
    {
        _categories.Remove(category);
    }

    #endregion

    #region Tags

    private void AddTag(string tag)
    {
        _tags.Add(tag);
    }

    private void RemoveTag(string tag)
    {
        _tags.Remove(tag);
    }

    #endregion
}

public class ReminderIdListConverter() : ValueConverter<List<ReminderId>, string>(
    list => JsonSerializer.Serialize(list, (JsonSerializerOptions)null!),
    json => JsonSerializer.Deserialize<List<ReminderId>>(json, new JsonSerializerOptions()) ?? new List<ReminderId>());
