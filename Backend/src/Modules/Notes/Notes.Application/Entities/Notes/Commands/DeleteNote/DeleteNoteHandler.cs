using BuildingBlocks.Application.Data;
using Notes.Application.Entities.Notes.Exceptions;

namespace Notes.Application.Entities.Notes.Commands.DeleteNote;

public class DeleteNoteHandler(INotesService notesService) : ICommandHandler<DeleteNoteCommand, DeleteNoteResult>
{
    public async Task<DeleteNoteResult> Handle(DeleteNoteCommand command, CancellationToken cancellationToken)
    {
        var note = await notesService.GetNoteBaseByIdAsync(command.Id, cancellationToken);

        if (note is null)
            throw new NoteNotFoundException(command.Id.ToString());

        await notesService.DeleteNote(command.Id, cancellationToken);

        return new DeleteNoteResult(true);
    }
}
