using Notes.Application.Entities.Notes.Exceptions;
using Notes.Application.Entities.Notes.Extensions;

namespace Notes.Application.Entities.Notes.Queries.GetNoteById;

internal class GetNoteByIdHandler(INotesDbContext dbContext) : IQueryHandler<GetNoteByIdQuery, GetNoteByIdResult>
{
    public async Task<GetNoteByIdResult> Handle(GetNoteByIdQuery query, CancellationToken cancellationToken)
    {
        var note = await dbContext.Notes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == NoteId.Of(Guid.Parse(query.Id)), cancellationToken);

        if (note is null)
            throw new NoteNotFoundException(query.Id);

        return new GetNoteByIdResult(note.ToNoteDto());
    }
}
