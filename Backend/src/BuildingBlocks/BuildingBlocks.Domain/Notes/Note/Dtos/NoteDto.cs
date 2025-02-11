namespace BuildingBlocks.Domain.Notes.Note.Dtos;

public class NoteDto(
    string Id,
    string Title,
    string Content,
    DateTime Timestamp,
    int Importance);
