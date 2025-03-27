using BuildingBlocks.Application.Data;
using Nodes.Application.Entities.Nodes.Exceptions;

namespace Nodes.Application.Entities.Nodes.Commands.DeleteNode;

public class DeleteNodeHandler(INodesService nodesService) : ICommandHandler<DeleteNodeCommand, DeleteNodeResult>
{
    public async Task<DeleteNodeResult> Handle(DeleteNodeCommand command, CancellationToken cancellationToken)
    {
        var node = await nodesService.GetNodeBaseByIdAsync(command.Id, cancellationToken);

        if (node is null)
            throw new NodeNotFoundException(command.Id.ToString());

        await nodesService.DeleteNode(node.Id, cancellationToken);

        return new DeleteNodeResult(true);
    }
}
