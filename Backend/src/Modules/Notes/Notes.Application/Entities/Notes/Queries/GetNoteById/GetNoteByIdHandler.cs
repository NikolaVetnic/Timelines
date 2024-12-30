using Notes.Application.Entities.Notes.Exceptions;
using Notes.Application.Entities.Notes.Extensions;

namespace Notes.Application.Entities.Notes.Queries.GetNoteById;

internal class GetNoteByIdHandler(INotesDbContext dbContext) : IQueryHandler<GetNoteByIdQuery, GetNoteByIdResult>
{
    public async Task<GetNoteByIdResult> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var note = await dbContext.Notes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == NoteId.Of(Guid.Parse(request.Id)), cancellationToken);

        if (note is null)
            throw new NoteNotFoundException(request.Id);

        return new GetNoteByIdResult(note.ToNoteDto());
    }
}
