using BuildingBlocks.Domain.Notes.Note.Dtos;

namespace Notes.Application.Entities.Notes.Extensions;

public static class NoteExtensions
{
    public static NoteDto ToNoteDto(this Note note)
    {
        return new NoteDto(
            note.Id.ToString(),
            note.Title,
            note.Content,
            note.Timestamp,
            note.Importance);
    }

    public static IEnumerable<NoteDto> ToNodeDtoList(this IEnumerable<Note> notes)
    {
        return notes.Select(ToNoteDto);
    }
}
