using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using Nodes.Application.Data.Abstractions;

namespace Nodes.Application.Entities.Nodes.Commands.CreateNode;

internal class CreateNodeHandler(ICurrentUser currentUser, INodesRepository nodesRepository, ITimelinesService timelineService) : ICommandHandler<CreateNodeCommand, CreateNodeResult>
{
    public async Task<CreateNodeResult> Handle(CreateNodeCommand command, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId!;
        var node = command.ToNode(userId);

        await nodesRepository.CreateNodeAsync(node, cancellationToken);
        await timelineService.AddNode(node.TimelineId, node.Id, cancellationToken);

        return new CreateNodeResult(node.Id);
    }
}

internal static class CreateNodeCommandExtensions
{
    public static Node ToNode(this CreateNodeCommand command, string userId)
    {
        return Node.Create(
            NodeId.Of(Guid.NewGuid()),
            command.Title,
            command.Description,
            command.PhaseId,
            command.Timestamp,
            command.Importance,
            userId,
            command.Categories,
            command.Tags,
            command.TimelineId
        );
    }
}
