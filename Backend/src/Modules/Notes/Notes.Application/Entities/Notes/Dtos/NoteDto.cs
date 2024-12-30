namespace Notes.Application.Entities.Notes.Dtos;

public record NoteDto(
    string Id,
    string Title,
    string Content,
    DateTime Timestamp,
    int Importance);
