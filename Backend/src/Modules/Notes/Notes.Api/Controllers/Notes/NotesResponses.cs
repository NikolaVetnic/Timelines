using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using System.Text.Json.Serialization;

namespace Notes.Api.Controllers.Notes;

public record CreateNoteResponse(NoteId Id);

public record GetNoteByIdResponse([property: JsonPropertyName("note")] NoteDto NoteDto);

public record ListNotesResponse(PaginatedResult<NoteDto> Notes);

public record UpdateNoteResponse(NoteDto Note);

public record DeleteNoteResponse(bool NoteDeleted);
