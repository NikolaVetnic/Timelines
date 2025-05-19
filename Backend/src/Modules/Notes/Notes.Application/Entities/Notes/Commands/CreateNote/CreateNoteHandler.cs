using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using Notes.Application.Data.Abstractions;

namespace Notes.Application.Entities.Notes.Commands.CreateNote;

internal class CreateNoteHandler(ICurrentUser currentUser, INotesRepository notesRepository, INodesService nodesService)
    : ICommandHandler<CreateNoteCommand, CreateNoteResult>
{
    public async Task<CreateNoteResult> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId!;
        var note = command.ToNote(userId);

        await nodesService.AddNote(note.NodeId, note.Id, cancellationToken);
        await notesRepository.AddNoteAsync(note, cancellationToken);

        return new CreateNoteResult(note.Id);
    }
}

internal static class CreateNoteCommandExtensions
{
    public static Note ToNote(this CreateNoteCommand command, string userId)
    {
        return Note.Create(
            NoteId.Of(Guid.NewGuid()),
            command.Title,
            command.Content,
            command.Timestamp,
            userId,
            command.SharedWith,
            command.IsPublic,
            command.NodeId
        );
    }
}
