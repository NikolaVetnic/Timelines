using BuildingBlocks.Domain.Notes.Note.Events;

namespace Notes.Domain.Models;

public class Note : Aggregate<NoteId>
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required DateTime Timestamp { get; set; }
    public required int Importance { get; set; }

    #region Note

    public static Note Create(NoteId id, string title, string content,
        DateTime timestamp, int importance)
    {
        var note = new Note
        {
            Id = id,
            Title = title,
            Content = content,
            Timestamp = timestamp,
            Importance = importance
        };

        note.AddDomainEvent(new NoteCreatedEvent(note.Id));

        return note;
    }

    public void Update(string title, string content, DateTime timestamp,
        int importance)
    {
        Title = title;
        Content = content;
        Timestamp = timestamp;
        Importance = importance;

        AddDomainEvent(new NoteUpdatedEvent(Id));
    }

    #endregion
}
