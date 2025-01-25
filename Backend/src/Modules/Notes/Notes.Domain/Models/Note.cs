using Notes.Domain.Events;

namespace Notes.Domain.Models;

public class Note : Aggregate<NoteId>
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required DateTime Timestamp { get; set; }
    public required List<Note> Related { get; set; }
    public required string[] SharedWith { get; set; }
    public required bool IsPublic { get; set; }

    #region Note

    public static Note Create(NoteId id, string title, string content,
        DateTime timestamp, List<Note> related, string[] sharedWith, bool isPublic)
    {
        var note = new Note
        {
            Id = id,
            Title = title,
            Content = content,
            Timestamp = timestamp,
            Related = related,
            SharedWith = sharedWith,
            IsPublic = isPublic
        };

        note.AddDomainEvent(new NoteCreatedEvent(note));

        return note;
    }

    public void Update(string title, string content, DateTime timestamp,
        List<Note> related, string[] sharedWith, bool isPublic)
    {
        Title = title;
        Content = content;
        Timestamp = timestamp;
        Related = related;
        SharedWith = sharedWith;
        IsPublic = isPublic;

        AddDomainEvent(new NoteUpdatedEvent(this));
    }

    #endregion
}
