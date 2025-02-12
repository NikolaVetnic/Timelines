using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace Notes.Application.Data.Abstractions;

public interface INotesRepository
{
    Task<Note> GetNoteByIdAsync(NoteId noteId, CancellationToken cancellationToken);
    Task UpdateNoteAsync(Note note, CancellationToken cancellationToken);
}
