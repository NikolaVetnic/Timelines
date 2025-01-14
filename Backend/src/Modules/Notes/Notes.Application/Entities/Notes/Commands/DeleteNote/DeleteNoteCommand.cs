namespace Notes.Application.Entities.Notes.Commands.DeleteNote;

public record DeleteNoteCommand(string Id) : ICommand<DeleteNoteResult>;

public record DeleteNoteResult(bool NoteDeleted);

public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
