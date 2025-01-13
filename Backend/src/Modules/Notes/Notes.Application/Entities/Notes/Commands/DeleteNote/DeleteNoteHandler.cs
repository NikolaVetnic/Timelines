using Notes.Application.Entities.Notes.Exceptions;

namespace Notes.Application.Entities.Notes.Commands.DeleteNote;

public class DeleteNoteHandler(INotesDbContext dbContext) : ICommandHandler<DeleteNoteCommand, DeleteNoteResult>
{
    public async Task<DeleteNoteResult> Handle(DeleteNoteCommand command, CancellationToken cancellationToken)
    {
        var note = await dbContext.Notes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == NoteId.Of(Guid.Parse(command.NoteId)), cancellationToken);

        if (note is null)
            throw new NoteNotFoundException(command.NoteId);

        dbContext.Notes.Remove(note);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteNoteResult(true);
    }
}
