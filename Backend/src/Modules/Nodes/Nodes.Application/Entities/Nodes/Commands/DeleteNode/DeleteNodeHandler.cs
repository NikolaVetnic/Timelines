using Nodes.Application.Entities.Nodes.Exceptions;

namespace Nodes.Application.Entities.Nodes.Commands.DeleteNode;

public class DeleteFileAssetHandler(INodesDbContext dbContext) : ICommandHandler<DeleteNodeCommand, DeleteNodeResult>
{
    public async Task<DeleteNodeResult> Handle(DeleteNodeCommand command, CancellationToken cancellationToken)
    {
        var node = await dbContext.Nodes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == NodeId.Of(Guid.Parse(command.NodeId)), cancellationToken);

        if (node is null)
            throw new NodeNotFoundException(command.NodeId);


        dbContext.Nodes.Remove(node);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteNodeResult(true);
    }
}
