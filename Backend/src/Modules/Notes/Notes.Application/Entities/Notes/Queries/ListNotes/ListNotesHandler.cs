using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using Notes.Application.Entities.Notes.Extensions;

namespace Notes.Application.Entities.Notes.Queries.ListNotes;

internal class ListNotesHandler(INotesDbContext dbContext) : IQueryHandler<ListNotesQuery, ListNotesResult>
{
    public async Task<ListNotesResult> Handle(ListNotesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Notes.LongCountAsync(cancellationToken);

        var notes = await dbContext.Notes
            .AsNoTracking()
            .OrderBy(n => n.Timestamp)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        return new ListNotesResult(
            new PaginatedResult<NoteDto>(
                pageIndex,
                pageSize,
                totalCount,
                notes.ToNodeDtoList()));
    }
}
