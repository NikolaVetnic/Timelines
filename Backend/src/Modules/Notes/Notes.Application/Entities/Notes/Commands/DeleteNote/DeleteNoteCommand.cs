namespace Notes.Application.Entities.Notes.Commands.DeleteNote;

public record DeleteNoteCommand(string NoteId) : ICommand<DeleteNoteResult>;

public record DeleteNoteResult(bool NoteDeleted);

public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(x => x.NoteId).NotEmpty().WithMessage("NoteId is required");
    }
}
