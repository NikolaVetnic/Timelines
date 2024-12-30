using BuildingBlocks.Domain.ValueObjects.Ids;
using FluentValidation;
using Notes.Application.Entities.Notes.Dtos;

namespace Notes.Application.Entities.Notes.Commands.CreateNote;

public record CreateNoteCommand(NoteDto Note) : ICommand<CreateNoteResult>;

public record CreateNoteResult(NoteId Id);

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(x => x.Note.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Note.Content).NotEmpty().WithMessage("Content is required.");
        RuleFor(x => x.Note.Importance).NotEmpty().WithMessage("Importance level is required.");
    }
}
