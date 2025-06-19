using BuildingBlocks.Application.Data;
using Notes.Application.Entities.Notes.Exceptions;

namespace Notes.Application.Entities.Notes.Commands.ReviveNote;

internal class ReviveNoteHandler(INotesService notesService) : ICommandHandler<ReviveNoteCommand, ReviveNoteResult>
{
    public async Task<ReviveNoteResult> Handle(ReviveNoteCommand command, CancellationToken cancellationToken)
    {
        var note = await notesService.GetNoteBaseByIdAsync(command.Id, cancellationToken);

        if (note is null)
            throw new NoteNotFoundException(command.Id.ToString());

        await notesService.ReviveNote(command.Id, cancellationToken);

        return new ReviveNoteResult(true);
    }
}
