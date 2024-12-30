using Notes.Application.Entities.Notes.Dtos;

namespace Notes.Application.Entities.Notes.Extensions;

public static class NoteExtensions
{
    public static NoteDto ToNoteDto(this Note node)
    {
        return new NoteDto(
            node.Id.ToString(),
            node.Title,
            node.Content,
            node.Timestamp,
            node.Importance);
    }

    public static IEnumerable<NoteDto> ToNodeDtoList(this IEnumerable<Note> nodes)
    {
        return nodes.Select(ToNoteDto);
    }
}
