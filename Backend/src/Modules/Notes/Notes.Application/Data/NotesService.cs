using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using Mapster;
using Notes.Application.Data.Abstractions;
using Notes.Application.Entities.Notes.Extensions;

namespace Notes.Application.Data;

public class NotesService(INotesRepository notesRepository, IServiceProvider serviceProvider) : INotesService
{
    public async Task<NoteDto> GetNoteByIdAsync(NoteId noteId, CancellationToken cancellationToken)
    {
        var note = await notesRepository.GetNoteByIdAsync(noteId, cancellationToken);
        var noteDto = note.ToNoteDto();

        return noteDto; 
    }

    public async Task<NoteBaseDto> GetNoteBaseByIdAsync(NoteId noteId, CancellationToken cancellationToken)
    {
        var note = await notesRepository.GetNoteByIdAsync(noteId, cancellationToken);
        var noteBaseDto = note.Adapt<NoteBaseDto>();

        return noteBaseDto;
    }
}
