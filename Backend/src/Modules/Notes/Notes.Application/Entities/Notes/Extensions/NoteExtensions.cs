using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Notes.Note.Dtos;

namespace Notes.Application.Entities.Notes.Extensions;

public static class NoteExtensions
{
    public static NoteDto ToNoteDto(this Note note, NodeBaseDto node)
    {
        return new NoteDto(
            note.Id.ToString(),
            note.Title,
            note.Content,
            note.Timestamp,
            note.Owner,
            note.RelatedNotes,
            note.SharedWith,
            note.IsPublic,
            node);
    }
}
