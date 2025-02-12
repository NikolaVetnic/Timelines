using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace Notes.Application.Entities.Notes.Commands.CreateNote;

public record CreateNoteCommand : ICommand<CreateNoteResult>
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required DateTime Timestamp { get; set; }
    public required string Owner { get; set; }
    public List<string> SharedWith { get; set; }
    public required bool IsPublic { get; set; }
    public required NodeId NodeId { get; set; }
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
        RuleFor(x => x.NodeId).NotEmpty().WithMessage("NodeId is required.");
    }
}
