namespace Notes.Application.Entities.Notes.Queries.GetNoteById;

public record GetNoteByIdQuery(string Id) : IQuery<GetNoteByIdResult>;

public record GetNoteByIdResult(NoteDto NoteDto);
