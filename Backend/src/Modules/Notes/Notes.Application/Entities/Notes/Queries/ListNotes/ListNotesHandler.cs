﻿using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Notes.Note.Dtos;

namespace Notes.Application.Entities.Notes.Queries.ListNotes;

internal class ListNotesHandler(INotesService notesService)
    : IQueryHandler<ListNotesQuery, ListNotesResult>
{
    public async Task<ListNotesResult> Handle(ListNotesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var notes = await notesService.ListNotesPaginated(pageIndex, pageSize, cancellationToken);

        return new ListNotesResult(
            new PaginatedResult<NoteDto>(
                pageIndex,
                pageSize,
                notes.Count,
                notes));
    }
}
