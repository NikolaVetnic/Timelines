﻿using System.Text.Json;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.Events;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
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
    public required DateTime Timestamp { get; set; }
    public required int Importance { get; set; }
    public required string OwnerId { get; set; }

    public required TimelineId TimelineId { get; set; }
    public List<FileAssetId> FileAssetIds { get; set; } = [];
    public List<NoteId> NoteIds { get; set; } = [];
    public List<ReminderId> ReminderIds { get; set; } = [];

    #region Node

    public static Node Create(NodeId id, string title, string description,
        DateTime timestamp, int importance, string ownerId, List<string> categories, List<string> tags, TimelineId timelineId)
    {
        var node = new Node
        {
            Id = id,
            Title = title,
            Description = description,
            Timestamp = timestamp,
            Importance = importance,
            OwnerId = ownerId,
            TimelineId = timelineId
        };

        foreach (var category in categories)
            node.AddCategory(category);

        foreach (var tag in tags)
            node.AddTag(tag);

        node.ReminderIds = [];
        node.FileAssetIds = [];
        node.NoteIds = [];

        node.AddDomainEvent(new NodeCreatedEvent(node.Id));

        return node;
    }

    public void Update(string title, string description, DateTime timestamp,
        int importance, string ownerId, PhaseId phaseId)
    {
        Title = title;
        Description = description;
        Timestamp = timestamp;
        Importance = importance;
        OwnerId = ownerId;
        AddDomainEvent(new NodeUpdatedEvent(Id));
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

    #region FileAssets

    public void AddFileAsset(FileAssetId fileAssetId)
    {
        if (!FileAssetIds.Contains(fileAssetId))
            FileAssetIds.Add(fileAssetId);
    }

    public void RemoveFileAsset(FileAssetId fileAssetId)
    {
        if (FileAssetIds.Contains(fileAssetId))
            FileAssetIds.Remove(fileAssetId);
    }

    #endregion

    #region Notes

    public void AddNote(NoteId noteId)
    {
        if (!NoteIds.Contains(noteId))
            NoteIds.Add(noteId);
    }

    public void RemoveNote(NoteId noteId)
    {
        if (NoteIds.Contains(noteId))
            NoteIds.Remove(noteId);
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
}
public class ReminderIdListConverter() : ValueConverter<List<ReminderId>, string>(
    list => JsonSerializer.Serialize(list, (JsonSerializerOptions)null!),
    json => JsonSerializer.Deserialize<List<ReminderId>>(json, new JsonSerializerOptions()) ?? new List<ReminderId>());

public class FileAssetIdListConverter() : ValueConverter<List<FileAssetId>, string>(
    list => JsonSerializer.Serialize(list, (JsonSerializerOptions)null!),
    json => JsonSerializer.Deserialize<List<FileAssetId>>(json, new JsonSerializerOptions()) ?? new List<FileAssetId>());

public class NoteIdListConverter() : ValueConverter<List<NoteId>, string>(
    list => JsonSerializer.Serialize(list, (JsonSerializerOptions)null!),
    json => JsonSerializer.Deserialize<List<NoteId>>(json, new JsonSerializerOptions()) ?? new List<NoteId>());
