using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Notes.Note.Dtos;

namespace Nodes.Application.Entities.Nodes.Queries.ListNotesByNodeId;

internal class ListNotesByNodeIdHandler(INotesService notesService)
    : IQueryHandler<ListNotesByNodeIdQuery, ListNotesByNodeIdResult>
{
    public async Task<ListNotesByNodeIdResult> Handle(ListNotesByNodeIdQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var notes = await notesService.ListNotesByNodeIdPaginated(query.Id, pageIndex, pageSize, cancellationToken);

        return new ListNotesByNodeIdResult(
            new PaginatedResult<NoteBaseDto>(
                pageIndex,
                pageSize,
                notes.Count,
                notes));
    }
}
