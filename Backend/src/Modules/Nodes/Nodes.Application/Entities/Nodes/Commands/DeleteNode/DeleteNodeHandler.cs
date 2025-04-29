using BuildingBlocks.Application.Data;
using Nodes.Application.Data.Abstractions.Nodes;
using Nodes.Application.Entities.Nodes.Exceptions;

namespace Nodes.Application.Entities.Nodes.Commands.DeleteNode;

public class DeleteNodeHandler(IFilesService filesService, INotesService notesService, IRemindersService remindersService, INodesRepository nodesRepository) : ICommandHandler<DeleteNodeCommand, DeleteNodeResult>
{
    public async Task<DeleteNodeResult> Handle(DeleteNodeCommand command, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(command.Id, cancellationToken);

        if (node is null)
            throw new NodeNotFoundException(command.Id.ToString());

        await filesService.DeleteFiles(node.Id, node.FileAssetIds, cancellationToken);
        await notesService.DeleteNotes(node.Id, node.NoteIds, cancellationToken);
        await remindersService.DeleteReminders(node.Id, node.ReminderIds, cancellationToken);
        await nodesRepository.DeleteNode(command.Id, cancellationToken);

        return new DeleteNodeResult(true);
    }
}
