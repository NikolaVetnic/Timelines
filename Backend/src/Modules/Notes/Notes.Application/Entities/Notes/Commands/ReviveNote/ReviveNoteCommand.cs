using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace Notes.Application.Entities.Notes.Commands.ReviveNote;

public record ReviveNoteCommand(NoteId Id) : ICommand<ReviveNoteResponse>
{
    public ReviveNoteCommand(string Id) : this(NoteId.Of(Guid.Parse(Id))) { }
}

public record ReviveNoteResponse(bool NoteRevived);

public class ReviveNoteCommandValidator : AbstractValidator<ReviveNoteCommand>
{
    public ReviveNoteCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
