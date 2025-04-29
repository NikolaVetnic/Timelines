using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using Notes.Application.Data.Abstractions;
using Notes.Application.Entities.Notes.Exceptions;
using Notes.Application.Entities.Notes.Extensions;

namespace Notes.Application.Entities.Notes.Commands.UpdateNote;

internal class UpdateNoteHandler(INotesRepository notesRepository, INodesService nodesService) : ICommandHandler<UpdateNoteCommand, UpdateNoteResult>
{
    public async Task<UpdateNoteResult> Handle(UpdateNoteCommand command, CancellationToken cancellationToken)
    {
        var note = await notesRepository.GetNoteByIdAsync(command.Id, cancellationToken);
        
        if (note is null)
            throw new NoteNotFoundException(command.Id.ToString());
        
        note.Title = command.Title ?? note.Title;
        note.Content = command.Content ?? note.Content;
        note.Timestamp = command.Timestamp ?? note.Timestamp;
        note.SharedWith = command.SharedWith ?? note.SharedWith;
        note.IsPublic = command.IsPublic ?? note.IsPublic;
        
        var node = await nodesService.GetNodeByIdAsync(
            command.NodeId ?? note.NodeId, cancellationToken);

        if (node.Id == null)
            throw new NotFoundException($"Related with id {command.NodeId} not found");

        note.NodeId = NodeId.Of(Guid.Parse(node.Id));

        await notesRepository.UpdateNoteAsync(note, cancellationToken);

        return new UpdateNoteResult(note.ToNoteDto(node));
    }
}
