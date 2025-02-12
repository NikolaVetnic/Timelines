using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace Notes.Application.Entities.Notes.Commands.CreateNote;

public record CreateNoteCommand : ICommand<CreateNoteResult>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public string Owner { get; set; }
    public List<string> SharedWith { get; set; }
    public bool IsPublic { get; set; }
}

public record CreateNoteResult(NoteId Id);

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
        RuleFor(x => x.Timestamp).NotEmpty().WithMessage("Timestamp is required.");
        RuleFor(x => x.Owner).NotEmpty().WithMessage("Owner is required.");
        RuleFor(x => x.IsPublic).NotEmpty().WithMessage("Please specify whether the note is public or private.");
    }
}
