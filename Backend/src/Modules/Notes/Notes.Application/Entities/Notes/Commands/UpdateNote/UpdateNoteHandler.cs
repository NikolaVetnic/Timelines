using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using Notes.Application.Data.Abstractions;
using Notes.Application.Entities.Notes.Exceptions;
using Notes.Application.Entities.Notes.Extensions;

namespace Notes.Application.Entities.Notes.Commands.UpdateNote;

internal class UpdateNoteHandler(INotesRepository notesRepository, INodesService nodesService)
    : ICommandHandler<UpdateNoteCommand, UpdateNoteResult>
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

        var node = await UpdateNode(note, command.NodeId, cancellationToken);

        await notesRepository.UpdateNoteAsync(note, cancellationToken);

        return new UpdateNoteResult(note.ToNoteDto(node));
    }

    private async Task<NodeDto> UpdateNode(Note note, NodeId? id, CancellationToken cancellationToken)
    {
        var node = await nodesService.GetNodeByIdAsync(note.NodeId, cancellationToken);

        if (id is null)
            return node;

        try
        {
            node = await nodesService.GetNodeByIdAsync(id, cancellationToken);

            if (node.Id is not null)
                note.NodeId = NodeId.Of(Guid.Parse(node.Id));
        }
        catch (Exception)
        {
            // ToDo: Replace this with proper logger (Serilog?)
            Console.WriteLine($"Related node with ID {id} not found");
        }

        return node;
    }
}