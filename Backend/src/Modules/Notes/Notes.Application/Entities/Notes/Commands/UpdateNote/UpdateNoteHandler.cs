using Notes.Application.Entities.Notes.Exceptions;

namespace Notes.Application.Entities.Notes.Commands.UpdateNote;

public class UpdateNodeHandler(INotesDbContext dbContext) : ICommandHandler<UpdateNoteCommand, UpdateNoteResult>
{
    public async Task<UpdateNoteResult> Handle(UpdateNoteCommand command, CancellationToken cancellationToken)
    {
        var note = await dbContext.Notes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == NoteId.Of(Guid.Parse(command.Note.Id)), cancellationToken);

        if (note is null)
            throw new NoteNotFoundException(command.Note.Id);

        UpdateNoteWithNewValues(note, command.Note);

        dbContext.Notes.Update(note);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateNoteResult(true);
    }

    private static void UpdateNoteWithNewValues(Note note, NoteDto noteDto)
    {
        note.Update(
            noteDto.Title,
            noteDto.Content,
            noteDto.Timestamp,
            noteDto.Importance);
    }
}
