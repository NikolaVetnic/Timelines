using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface INotesService
{
    Task<List<NoteDto>> ListNotesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<NoteDto> GetNoteByIdAsync(NoteId noteId, CancellationToken cancellationToken);
    Task<NoteBaseDto> GetNoteBaseByIdAsync(NoteId noteId, CancellationToken cancellationToken);
    Task DeleteNote(NoteId noteId, CancellationToken cancellationToken);
    Task<List<NoteBaseDto>> GetNotesBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
    Task<long> CountNotesAsync(CancellationToken cancellationToken);
}
