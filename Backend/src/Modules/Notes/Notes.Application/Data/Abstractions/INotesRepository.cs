using System.Linq.Expressions;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace Notes.Application.Data.Abstractions;

public interface INotesRepository
{
    Task AddNoteAsync(Note note, CancellationToken cancellationToken);

    Task<List<Note>> ListNotesPaginatedAsync(Expression<Func<Note, bool>> predicate, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<Note>> ListFlaggedForDeletionNotesPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<Note>> ListNotesByNodeIdPaginatedAsync(NodeId nodeId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<long> CountAllNotesAsync(CancellationToken cancellationToken);
    Task<long> CountAllNotesByNodeIdAsync(NodeId nodeId, CancellationToken cancellationToken);

    Task<Note> GetNoteByIdAsync(NoteId noteId, CancellationToken cancellationToken);
    Task<List<Note>> GetNotesByIdsAsync(IEnumerable<NoteId> noteIds, CancellationToken cancellationToken);

    Task UpdateNoteAsync(Note note, CancellationToken cancellationToken);
    Task DeleteNote(NoteId noteId, CancellationToken cancellationToken);
    Task DeleteNotes(IEnumerable<NoteId> noteIds, CancellationToken cancellationToken);
    Task DeleteNotesByNodeIds(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);

    Task ReviveNoteAsync(NoteId noteId, CancellationToken cancellationToken);

    Task<IEnumerable<Note>> GetNotesBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
}
