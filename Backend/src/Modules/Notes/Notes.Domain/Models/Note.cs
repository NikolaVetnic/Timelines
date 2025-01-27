using Notes.Domain.Events;

namespace Notes.Domain.Models;

public class Note : Aggregate<NoteId>
{
    private readonly List<Note> _related = [];

    public IReadOnlyList<Note> Related => _related.AsReadOnly();

    public required string Title { get; set; }
    public required string Content { get; set; }
    public required DateTime Timestamp { get; set; }
    public required string[] SharedWith { set; get; }
    public required bool IsPublic { get; set; }

    #region Note

    public static Note Create(NoteId id, string title, string content,
        DateTime timestamp, List<Note> relatedNotes, string[] sharedWith, bool isPublic)
    {
        var note = new Note
        {
            Id = id,
            Title = title,
            Content = content,
            Timestamp = timestamp,
            SharedWith = sharedWith,
            IsPublic = isPublic
        };

        foreach (var relatedNote in relatedNotes)
            note.AddRelatedNote(relatedNote);

        note.AddDomainEvent(new NoteCreatedEvent(note));

        return note;
    }

    public void Update(string title, string content, DateTime timestamp,
        List<Note> related, string[] sharedWith, bool isPublic)
    {
        Title = title;
        Content = content;
        Timestamp = timestamp;
        SharedWith = sharedWith;
        IsPublic = isPublic;

        AddDomainEvent(new NoteUpdatedEvent(this));
    }

    #endregion

    #region SharedWith

    private void AddRelatedNote(Note note)
    {
        _related.Add(note);
    }

    #endregion
}
