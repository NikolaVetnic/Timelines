﻿using BuildingBlocks.Domain.Notes.Note.Events;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace Notes.Domain.Models;

public class Note : Aggregate<NoteId>
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required DateTime Timestamp { get; set; }
    public required string OwnerId { get; set; }
    public List<NoteId> RelatedNotes { get; set; } = [];
    public List<string> SharedWith { get; set; } = [];
    public required bool IsPublic { get; set; }
    public required NodeId NodeId { get; set; }

    #region Note

    public static Note Create(NoteId id, string title, string content, DateTime timestamp, string ownerId, List<string>? sharedWith, bool isPublic, NodeId nodeId)
    {
        var note = new Note
        {
            Id = id,
            Title = title,
            Content = content,
            Timestamp = timestamp,
            OwnerId = ownerId,
            IsPublic = isPublic,
            NodeId = nodeId
        };

        foreach (var user in sharedWith)
            note.AddUser(user);

        note.RelatedNotes = [];

        note.AddDomainEvent(new NoteCreatedEvent(note.Id));

        return note;
    }

    public void Update(string title, string content, DateTime timestamp, string ownerId, bool isPublic)
    {
        Title = title;
        Content = content;
        Timestamp = timestamp;
        OwnerId = ownerId;
        IsPublic = isPublic;

        AddDomainEvent(new NoteUpdatedEvent(Id));
    }

    #endregion

    #region RelatedNotes

    public void AddRelatedNotes(NoteId noteId)
    {
        if (!RelatedNotes.Contains(noteId))
            RelatedNotes.Add(noteId);
    }

    public void RemoveRelatedNotes(NoteId noteId)
    {
        if (RelatedNotes.Contains(noteId))
            RelatedNotes.Remove(noteId);
    }

    #endregion

    #region SharedWith

    private void AddUser(string user)
    {
        SharedWith.Add(user);
    }

    private void RemoveUser(string user)
    {
        SharedWith.Remove(user);
    }

    #endregion
}

public class RelatedNoteIdListConverter() : ValueConverter<List<NoteId>, string>(
    list => JsonSerializer.Serialize(list, (JsonSerializerOptions)null!),
    json => JsonSerializer.Deserialize<List<NoteId>>(json, new JsonSerializerOptions()) ?? new List<NoteId>());

public class StringListConverter() : ValueConverter<List<string>, string>(
    list => JsonSerializer.Serialize(list, (JsonSerializerOptions)null!),
    json => JsonSerializer.Deserialize<List<string>>(json, new JsonSerializerOptions()) ?? new List<string>());
