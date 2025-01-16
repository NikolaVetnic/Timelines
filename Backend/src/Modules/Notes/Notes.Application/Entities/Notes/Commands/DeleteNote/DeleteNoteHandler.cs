using Notes.Application.Entities.Notes.Exceptions;

namespace Notes.Application.Entities.Notes.Commands.DeleteNote;

public class DeleteNoteHandler(INotesDbContext dbContext) : ICommandHandler<DeleteNoteCommand, DeleteNoteResult>
{
    public async Task<DeleteNoteResult> Handle(DeleteNoteCommand command, CancellationToken cancellationToken)
    {
        var note = await dbContext.Notes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == command.Id, cancellationToken);

        if (note is null)
            throw new NoteNotFoundException(command.Id.ToString());

        dbContext.Notes.Remove(note);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteNoteResult(true);
    }
}
