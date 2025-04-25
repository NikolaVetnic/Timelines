namespace BuildingBlocks.Domain.Notes.Note.Dtos;

public record NoteDto(
    string Id,
    string Title,
    string Content,
    DateTime Timestamp,
    int Importance);
