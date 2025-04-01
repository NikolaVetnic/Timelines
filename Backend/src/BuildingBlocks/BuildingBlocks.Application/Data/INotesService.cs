using BuildingBlocks.Domain.Notes.Note.Dtos;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface INotesService
{
    Task<NoteDto> GetNoteByIdAsync(NoteId noteId, CancellationToken cancellationToken);
    Task<NoteBaseDto> GetNoteBaseByIdAsync(NoteId noteId, CancellationToken cancellationToken);
    Task DeleteNote(NoteId noteId, CancellationToken cancellationToken);
}
