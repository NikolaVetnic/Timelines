using BuildingBlocks.Domain.ValueObjects.Ids;
using Nodes.Application.Data;
using Nodes.Domain.Models;

namespace Nodes.Application.Nodes.Commands.CreateNode;

public class CreateNodeHandler(INodesDbContext dbContext) :
    ICommandHandler<CreateNodeCommand, CreateNodeResult>
{
    public async Task<CreateNodeResult> Handle(CreateNodeCommand command, CancellationToken cancellationToken)
    {
        var node = Node.Create(
            NodeId.Of(Guid.NewGuid()),
            command.Node.Title,
            command.Node.Description,
            command.Node.Phase,
            command.Node.Timestamp,
            command.Node.Importance,
            command.Node.Categories,
            command.Node.Tags
        );
        
        dbContext.Nodes.Add(node);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateNodeResult(node.Id);
    }
}