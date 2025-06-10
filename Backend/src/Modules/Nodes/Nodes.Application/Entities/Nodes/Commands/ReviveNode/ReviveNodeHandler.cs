using Nodes.Application.Data.Abstractions;
using Nodes.Application.Entities.Nodes.Exceptions;

namespace Nodes.Application.Entities.Nodes.Commands.ReviveNode;

internal class ReviveNodeHandler(INodesRepository nodesRepository) : ICommandHandler<ReviveNodeCommand, ReviveNodeResult>
{
    public async Task<ReviveNodeResult> Handle(ReviveNodeCommand command, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(command.Id, cancellationToken);

        if (node is null)
            throw new NodeNotFoundException(command.Id.ToString());

        await nodesRepository.ReviveNode(command.Id, cancellationToken);

        return new ReviveNodeResult(true);
    }
}
