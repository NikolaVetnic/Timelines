using Notes.Application.Entities.Notes.Exceptions;
using Notes.Application.Entities.Notes.Extensions;

namespace Notes.Application.Entities.Notes.Queries.GetNoteById;

internal class GetNoteByIdHandler(INotesDbContext dbContext) : IQueryHandler<GetNoteByIdQuery, GetNoteByIdResult>
{
    public async Task<GetNoteByIdResult> Handle(GetNoteByIdQuery query, CancellationToken cancellationToken)
    {
        var noteId = query.Id.ToString();

        var note = await dbContext.Notes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == NoteId.Of(Guid.Parse(noteId)), cancellationToken);

        if (note is null)
            throw new NoteNotFoundException(noteId);

        return new GetNoteByIdResult(note.ToNoteDto());
    }
}
