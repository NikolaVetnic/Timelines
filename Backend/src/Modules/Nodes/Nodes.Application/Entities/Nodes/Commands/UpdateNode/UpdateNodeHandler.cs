using Nodes.Application.Entities.Nodes.Dtos;
using Nodes.Application.Entities.Nodes.Exceptions;

namespace Nodes.Application.Entities.Nodes.Commands.UpdateNode;

public class UpdateNodeHandler(INodesDbContext dbContext) : ICommandHandler<UpdateNodeCommand, UpdateNodeResult>
{
    public async Task<UpdateNodeResult> Handle(UpdateNodeCommand command, CancellationToken cancellationToken)
    {
        var node = await dbContext.Nodes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == NodeId.Of(Guid.Parse(command.Node.Id)), cancellationToken);

        if (node is null)
            throw new NodeNotFoundException(command.Node.Id);

        UpdateNodeWithNewValues(node, command.Node);

        dbContext.Nodes.Update(node);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateNodeResult(true);
    }

    private static void UpdateNodeWithNewValues(Node node, NodeDto nodeDto)
    {
        node.Update(
            nodeDto.Title,
            nodeDto.Description,
            nodeDto.Timestamp,
            nodeDto.Importance,
            nodeDto.Phase);
    }
}
