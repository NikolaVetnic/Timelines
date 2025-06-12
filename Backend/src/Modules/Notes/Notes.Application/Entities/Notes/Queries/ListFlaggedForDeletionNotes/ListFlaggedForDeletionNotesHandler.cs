using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Notes.Note.Dtos;

namespace Notes.Application.Entities.Notes.Queries.ListFlaggedForDeletionNotes;
internal class ListFlaggedForDeletionNotesHandler(INotesService notesService) : IQueryHandler<ListFlaggedForDeletionNotesQuery, ListFlaggedForDeletionNotesResult>
{
    public async Task<ListFlaggedForDeletionNotesResult> Handle(ListFlaggedForDeletionNotesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var notes = await notesService.ListFlaggedForDeletionNotesPaginated(pageIndex, pageSize, cancellationToken);

        var totalCount = notes.Count;

        return new ListFlaggedForDeletionNotesResult(
            new PaginatedResult<NoteDto>(
                pageIndex,
                pageSize,
                totalCount,
                notes));
    }
}
