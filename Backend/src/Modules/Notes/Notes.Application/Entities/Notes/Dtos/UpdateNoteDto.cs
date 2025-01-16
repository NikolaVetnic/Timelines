namespace Notes.Application.Entities.Notes.Dtos;

public record UpdateNoteDto(
    string Id,
    string? Title,
    string? Content,
    DateTime? Timestamp,
    int? Importance);
