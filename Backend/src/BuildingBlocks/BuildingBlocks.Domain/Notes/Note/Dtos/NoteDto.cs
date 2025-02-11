using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace BuildingBlocks.Domain.Notes.Note.Dtos;

public class NoteDto(
    string? id,
    string title,
    string content,
    DateTime timestamp,
    string owner,
    List<NoteId> relatedNotes,
    List<string> sharedWith,
    bool isPublic) : NoteBaseDto(id, title, content, timestamp, owner, relatedNotes, sharedWith, isPublic)
{
}
