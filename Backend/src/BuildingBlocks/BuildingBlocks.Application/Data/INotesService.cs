using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface INotesService
{
    Task<List<NoteDto>> ListNotesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<NoteBaseDto>> ListNotesByNodeIdPaginated(NodeId nodeId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<NoteDto> GetNoteByIdAsync(NoteId noteId, CancellationToken cancellationToken);
    Task<NoteBaseDto> GetNoteBaseByIdAsync(NoteId noteId, CancellationToken cancellationToken);
    Task<long> CountAllNotesAsync(CancellationToken cancellationToken);
    Task<long> CountAllNotesByNodeIdAsync(NodeId nodeId, CancellationToken cancellationToken);

    Task DeleteNote(NoteId noteId, CancellationToken cancellationToken);
    Task DeleteNotes(NodeId nodeId, IEnumerable<NoteId> noteIds, CancellationToken cancellationToken);
    Task DeleteNotesByNodeIds(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);

    Task<List<NoteBaseDto>> GetNotesBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
}
