using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Notes.Note.Dtos;

namespace Notes.Application.Entities.Notes.Queries.ListFlaggedForDeletionNotes;

public record ListFlaggedForDeletionNotesQuery(PaginationRequest PaginationRequest) : IQuery<ListFlaggedForDeletionNotesResult>;

public record ListFlaggedForDeletionNotesResult(PaginatedResult<NoteDto> Notes);
