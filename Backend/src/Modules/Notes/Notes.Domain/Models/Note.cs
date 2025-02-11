using BuildingBlocks.Domain.Notes.Note.Events;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace Notes.Domain.Models;

public class Note : Aggregate<NoteId>
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required DateTime Timestamp { get; set; }
    public required string Owner { get; set; }
    public List<NoteId> RelatedNotes { get; set; } = [];
    public List<string> SharedWith { get; set; } = [];
    public required bool IsPublic { get; set; }

    #region Note

    public static Note Create(NoteId id, string title, string content, DateTime timestamp, string owner, List<string> sharedWith, bool isPublic)
    {
        var note = new Note
        {
            Id = id,
            Title = title,
            Content = content,
            Timestamp = timestamp,
            Owner = owner,
            IsPublic = isPublic
        };

        foreach (var user in sharedWith)
            note.AddUser(user);

        note.RelatedNotes = [];

        note.AddDomainEvent(new NoteCreatedEvent(note.Id));

        return note;
    }

    public void Update(string title, string content, DateTime timestamp, string owner, bool isPublic)
    {
        Title = title;
        Content = content;
        Timestamp = timestamp;
        Owner = owner;
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
