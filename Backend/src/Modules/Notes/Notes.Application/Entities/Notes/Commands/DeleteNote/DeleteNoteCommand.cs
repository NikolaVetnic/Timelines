using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace Notes.Application.Entities.Notes.Commands.DeleteNote;

public record DeleteNoteCommand(NoteId Id) : ICommand<DeleteNoteResult>
{
    public DeleteNoteCommand(string Id) : this(NoteId.Of(Guid.Parse(Id))) { }
}

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
