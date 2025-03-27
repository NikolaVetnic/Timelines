using BuildingBlocks.Application.Data;
using Notes.Application.Data.Abstractions;
using Notes.Application.Entities.Notes.Exceptions;

namespace Notes.Application.Entities.Notes.Commands.DeleteNote;

public class DeleteNoteHandler(INotesDbContext dbContext, INodesService nodesService) : ICommandHandler<DeleteNoteCommand, DeleteNoteResult>
{
    public async Task<DeleteNoteResult> Handle(DeleteNoteCommand command, CancellationToken cancellationToken)
    {
        var note = await dbContext.Notes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == command.Id, cancellationToken);

        if (note is null)
            throw new NoteNotFoundException(command.Id.ToString());

        dbContext.Notes.Remove(note);
        await nodesService.RemoveNote(note.NodeId, note.Id, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteNoteResult(true);
    }
}
