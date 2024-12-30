using BuildingBlocks.Domain.ValueObjects.Ids;
using Notes.Application.Data;

namespace Notes.Application.Entities.Notes.Commands.CreateNote;

public class CreateNoteHandler(INotesDbContext dbContext)
    : ICommandHandler<CreateNoteCommand, CreateNoteResult>
{
    public async Task<CreateNoteResult> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
    {
        var note = Note.Create(
            NoteId.Of(Guid.NewGuid()),
            command.Note.Title,
            command.Note.Content,
            command.Note.Timestamp,
            command.Note.Importance
        );

        dbContext.Notes.Add(note);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateNoteResult(note.Id);
    }
}
