using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using Notes.Application.Data.Abstractions;
using Notes.Application.Entities.Notes.Exceptions;

namespace Notes.Infrastructure.Data.Repositories;

public class NotesRepository(INotesDbContext dbContext) : INotesRepository
{
    public async Task<Note> GetNoteByIdAsync(NoteId noteId, CancellationToken cancellationToken)
    {
        return await dbContext.Notes
                   .AsNoTracking()
                   .SingleOrDefaultAsync(n => n.Id == noteId, cancellationToken) ??
               throw new NoteNotFoundException(noteId.ToString());
    }

    public async Task UpdateNoteAsync(Note note, CancellationToken cancellationToken)
    {
        dbContext.Notes.Update(note);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
