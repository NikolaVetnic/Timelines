using BuildingBlocks.Application.Pagination;

namespace Notes.Application.Entities.Notes.Queries.ListNotes;

public record ListNotesQuery(PaginationRequest PaginationRequest) : IQuery<ListNotesResult>;

public record ListNotesResult(PaginatedResult<NoteDto> Notes);
