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

    private static void UpdateNoteWithNewValues(Note note, UpdateNoteDto noteDto)
    {
        if (noteDto.Title is not null)
            note.Title = noteDto.Title;

        if (noteDto.Content is not null)
            note.Content = noteDto.Content;

        if (noteDto.Timestamp.HasValue && noteDto.Timestamp.Value != DateTime.MinValue)
            note.Timestamp = noteDto.Timestamp.Value;

        if (noteDto.Importance.HasValue && noteDto.Importance != 0)
            note.Importance = noteDto.Importance.Value;
    }
}
