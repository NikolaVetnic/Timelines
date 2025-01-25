namespace Notes.Application.Entities.Notes.Dtos;

public record NoteDto(
    string Id,
    string Title,
    string Content,
    DateTime Timestamp,
    List<Note> Related,
    string[] SharedWith,
    bool IsPublic);
