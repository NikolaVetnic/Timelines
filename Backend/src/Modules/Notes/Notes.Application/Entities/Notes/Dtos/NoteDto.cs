namespace Notes.Application.Entities.Notes.Dtos;

public record NoteDto(
    string Title,
    string Content,
    DateTime Timestamp,
    int Importance);
