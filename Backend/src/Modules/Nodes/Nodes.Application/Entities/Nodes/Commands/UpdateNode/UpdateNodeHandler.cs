using Nodes.Application.Entities.Nodes.Exceptions;

namespace Nodes.Application.Entities.Nodes.Commands.UpdateNode;

public class UpdateNodeHandler(INodesDbContext dbContext) : ICommandHandler<UpdateNodeCommand, UpdateNodeResult>
{
    public async Task<UpdateNodeResult> Handle(UpdateNodeCommand command, CancellationToken cancellationToken)
    {
        var node = await dbContext.Nodes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == NodeId.Of(Guid.Parse(command.Id)), cancellationToken);

        if (node is null)
            throw new NodeNotFoundException(command.Id);

        UpdateNodeWithNewValues(node, command);

        dbContext.Nodes.Update(node);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateNodeResult(true);
    }

    private static void UpdateNodeWithNewValues(Node node, UpdateNodeCommand command)
    {
        node.Update(
            command.Title,
            command.Description,
            command.Timestamp,
            command.Importance,
            command.Phase);
    }
}
