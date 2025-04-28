using BuildingBlocks.Application.Data;
using Notes.Application.Entities.Notes.Exceptions;

namespace Notes.Application.Entities.Notes.Queries.GetNoteById;

internal class GetNoteByIdHandler(INotesService notesService) : IQueryHandler<GetNoteByIdQuery, GetNoteByIdResult>
{
    public async Task<GetNoteByIdResult> Handle(GetNoteByIdQuery query, CancellationToken cancellationToken)
    {
        var noteDto = await notesService.GetNoteByIdAsync(query.Id, cancellationToken);

        if (noteDto is null)
            throw new NoteNotFoundException(query.Id.ToString());

        return new GetNoteByIdResult(noteDto);
    }
}
