namespace BuildingBlocks.Domain.Notes.Note.Dtos;

public class NoteDto(
    string id,
    string title,
    string content,
    DateTime timestamp,
    int importance) : NoteBaseDto(id, title, content, timestamp, importance)
{

}
