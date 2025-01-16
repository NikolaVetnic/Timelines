namespace Notes.Application.Entities.Notes.Commands.UpdateNote;

public record UpdateNoteCommand(UpdateNoteDto Note) : ICommand<UpdateNoteResult>;

public record UpdateNoteResult(bool NoteUpdated);

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(x => x.Note.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Note.Title)
            .NotEmpty()
            .When(x => x.Note.Title is not null)
            .WithMessage("Title is required if provided.");
        RuleFor(x => x.Note.Content)
            .NotEmpty()
            .When(x => x.Note.Content is not null)
            .WithMessage("Content is required if provided.");
        RuleFor(x => x.Note.Importance)
            .NotEmpty()
            .When(x => x.Note.Importance != 0)
            .WithMessage("Importance level is required if provided.");
    }
}
