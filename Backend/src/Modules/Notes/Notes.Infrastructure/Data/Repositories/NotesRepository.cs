﻿using System.Linq.Expressions;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using Notes.Application.Data.Abstractions;
using Notes.Application.Entities.Notes.Exceptions;

namespace Notes.Infrastructure.Data.Repositories;

public class NotesRepository(ICurrentUser currentUser, INotesDbContext dbContext) : INotesRepository
{
    #region List

    public async Task<List<Note>> ListNotesPaginatedAsync(Expression<Func<Note, bool>> predicate, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.Notes
            .AsNoTracking()
            .Where(n => n.OwnerId == currentUser.UserId)
            .Where(predicate)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<long> CountNotesAsync(Expression<Func<Note, bool>> predicate, CancellationToken cancellationToken)
    {
        return await dbContext.Notes
            .Where(n => n.OwnerId == currentUser.UserId!)
            .Where(predicate)
            .LongCountAsync(cancellationToken);
    }

    #endregion

    #region Get

    public async Task<Note> GetNoteByIdAsync(NoteId noteId, CancellationToken cancellationToken)
    {
        return await dbContext.Notes
                   .AsNoTracking()
                   .SingleOrDefaultAsync(n => n.Id == noteId && n.OwnerId == currentUser.UserId!, cancellationToken) ??
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

    public async Task AddNoteAsync(Note note, CancellationToken cancellationToken)
    {
        dbContext.Notes.Add(note);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateNoteAsync(Note note, CancellationToken cancellationToken)
    {
        dbContext.Notes.Update(note);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteNote(NoteId noteId, CancellationToken cancellationToken)
    {
        var noteToDelete = await dbContext.Notes
            .FirstAsync(n => n.Id == noteId && n.OwnerId == currentUser.UserId!, cancellationToken);

        noteToDelete.MarkAsDeleted();

        dbContext.Notes.Update(noteToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteNotes(IEnumerable<NoteId> noteIds, CancellationToken cancellationToken)
    {
        var notesToDelete = await dbContext.Notes
            .Where(n => noteIds.Contains(n.Id))
            .ToListAsync(cancellationToken);

        foreach (var note in notesToDelete)
            note.MarkAsDeleted();

        dbContext.Notes.UpdateRange(notesToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteNotesByNodeIds(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        foreach (var nodeId in nodeIds)
        {
            var notesToDelete = await dbContext.Notes
                .Where(n => n.NodeId == nodeId)
                .ToListAsync(cancellationToken);

            foreach (var note in notesToDelete)
                note.MarkAsDeleted();

            dbContext.Notes.UpdateRange(notesToDelete);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task ReviveNoteAsync(NoteId noteId, CancellationToken cancellationToken)
    {
        var noteToDelete = await dbContext.Notes
            .FirstAsync(n => n.Id == noteId && n.OwnerId == currentUser.UserId!, cancellationToken);

        noteToDelete.Revive();

        dbContext.Notes.Update(noteToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Note>> GetNotesBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        return await dbContext.Notes
            .AsNoTracking()
            .Where(n => nodeIds.Contains(n.NodeId) && n.OwnerId == currentUser.UserId! && n.IsDeleted == false)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
