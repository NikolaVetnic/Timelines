using BuildingBlocks.Application.Data;
using Nodes.Application.Data.Abstractions;
using Nodes.Application.Entities.Nodes.Exceptions;

namespace Nodes.Application.Entities.Nodes.Commands.DeleteNode;

public class DeleteNodeHandler(INotesService notesService, INodesRepository nodesRepository) : ICommandHandler<DeleteNodeCommand, DeleteNodeResult>
{
    public async Task<DeleteNodeResult> Handle(DeleteNodeCommand command, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(command.Id, cancellationToken);

        if (node is null)
            throw new NodeNotFoundException(command.Id.ToString());

        await notesService.DeleteNotes(node.Id, node.NoteIds, cancellationToken);
        await nodesRepository.DeleteNode(command.Id, cancellationToken);

        return new DeleteNodeResult(true);
    }
}
