using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using Notes.Application.Data.Abstractions;
using Notes.Application.Entities.Notes.Exceptions;

namespace Notes.Infrastructure.Data.Repositories;

public class NotesRepository(INotesDbContext dbContext) : INotesRepository
{
    #region List
    public async Task<List<Note>> ListNotesPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.Notes
            .AsNoTracking()
            .OrderBy(n => n.Timestamp)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<long> NoteCountAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Notes.LongCountAsync(cancellationToken);
    }

    #endregion

    #region Get
    public async Task<Note> GetNoteByIdAsync(NoteId noteId, CancellationToken cancellationToken)
    {
        return await dbContext.Notes
                   .AsNoTracking()
                   .SingleOrDefaultAsync(n => n.Id == noteId, cancellationToken) ??
               throw new NoteNotFoundException(noteId.ToString());
    }

    public async Task<List<Note>> GetNotesByIdsAsync(IEnumerable<NoteId> noteIds, CancellationToken cancellationToken)
    {
        return await dbContext.Notes
            .AsNoTracking()
            .Where(n => noteIds.Contains(n.Id))
            .ToListAsync(cancellationToken);
    }
    #endregion

    public async Task UpdateNoteAsync(Note note, CancellationToken cancellationToken)
    {
        dbContext.Notes.Update(note);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveNote(Note note, CancellationToken cancellationToken)
    {
        dbContext.Notes.Remove(note);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteNote(NoteId noteId, CancellationToken cancellationToken)
    {
        var noteToDelete = await dbContext.Notes
            .FirstAsync(n => n.Id == noteId, cancellationToken);

        dbContext.Notes.Remove(noteToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Note>> GetNotesBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        return await dbContext.Notes
            .AsNoTracking()
            .Where(n => nodeIds.Contains(n.NodeId))
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
