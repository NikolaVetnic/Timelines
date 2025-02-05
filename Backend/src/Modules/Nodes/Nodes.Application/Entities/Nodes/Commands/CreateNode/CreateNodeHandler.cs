using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using Nodes.Application.Data.Abstractions;

namespace Nodes.Application.Entities.Nodes.Commands.CreateNode;

internal class CreateNodeHandler(INodesDbContext dbContext, ITimelinesService timelineService) : ICommandHandler<CreateNodeCommand, CreateNodeResult>
{
    public async Task<CreateNodeResult> Handle(CreateNodeCommand command, CancellationToken cancellationToken)
    {
        var node = command.ToNode();

        dbContext.Nodes.Add(node);
        await dbContext.SaveChangesAsync(cancellationToken);

        await timelineService.AddNode(node.TimelineId, node.Id, cancellationToken);

        return new CreateNodeResult(node.Id);
    }
}

internal static class CreateNodeCommandExtensions
{
    public static Node ToNode(this CreateNodeCommand command)
    {
        return Node.Create(
            NodeId.Of(Guid.NewGuid()),
            command.Title,
            command.Description,
            command.Phase,
            command.Timestamp,
            command.Importance,
            command.Categories,
            command.Tags,
            command.TimelineId
        );
    }
}
