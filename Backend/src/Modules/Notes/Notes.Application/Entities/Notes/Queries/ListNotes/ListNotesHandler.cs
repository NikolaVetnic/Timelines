using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using Notes.Application.Data.Abstractions;
using Notes.Application.Entities.Notes.Extensions;

namespace Notes.Application.Entities.Notes.Queries.ListNotes;

internal class ListNotesHandler(INotesDbContext dbContext, INodesService nodesService) : IQueryHandler<ListNotesQuery, ListNotesResult>
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

        var noteDtos = notes.Select(r =>
        {
            var node = nodesService.GetNodeBaseByIdAsync(r.NodeId, cancellationToken).GetAwaiter().GetResult();
            return r.ToNoteDto(node);
        }).ToList();

        return new ListNotesResult(
            new PaginatedResult<NoteDto>(
                pageIndex,
                pageSize,
                totalCount,
                noteDtos));
    }
}
