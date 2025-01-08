namespace Notes.Application.Entities.Notes.Commands.UpdateNote;

public record UpdateNoteCommand(NoteDto Note) : ICommand<UpdateNoteResult>;

public record UpdateNoteResult(bool NoteUpdated);

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(x => x.Note.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Note.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Note.Content).NotEmpty().WithMessage("Content is required.");
        RuleFor(x => x.Note.Importance).NotEmpty().WithMessage("Importance level is required.");
    }
}
