using Nodes.Application.Data;

namespace Nodes.Application.Entities.Nodes.Commands.CreateNode;

public class CreateNodeHandler(INodesDbContext dbContext) : ICommandHandler<CreateNodeCommand, CreateNodeResult>
{
    public async Task<CreateNodeResult> Handle(CreateNodeCommand command, CancellationToken cancellationToken)
    {
        var node = command.ToNode();

        dbContext.Nodes.Add(node);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateNodeResult(node.Id);
    }
}

internal static partial class CreateNodeCommandExtensions
{
    public static Node ToNode(this CreateNodeCommand command)
    {
        return Node.Create(
            NodeId.Of(Guid.NewGuid()),
            command.Node.Title,
            command.Node.Description,
            command.Node.Phase,
            command.Node.Timestamp,
            command.Node.Importance,
            command.Node.Categories,
            command.Node.Tags
        );
    }
}
