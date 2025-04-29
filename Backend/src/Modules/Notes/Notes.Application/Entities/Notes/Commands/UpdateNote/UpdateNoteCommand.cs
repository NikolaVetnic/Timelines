using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace Notes.Application.Entities.Notes.Commands.UpdateNote;

public record UpdateNoteCommand : ICommand<UpdateNoteResult>
{
    public required NoteId Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime? Timestamp { get; set; }
    public List<string>? SharedWith { get; set; }
    public bool? IsPublic { get; set; }
    public NodeId? NodeId { get; set; }
}

public record UpdateNoteResult(NoteDto Note);

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator() { }
}
