namespace Notes.Application.Dtos;

public record NoteDto(
    string Title,
    string Content,
    DateTime Timestamp,
    int Importance);
