using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Nodes.Application.Data.Abstractions;
using Nodes.Application.Entities.Nodes.Exceptions;
using Nodes.Application.Entities.Nodes.Extensions;

namespace Nodes.Application.Entities.Nodes.Commands.UpdateNode;

internal class UpdateNodeHandler(INodesRepository nodesRepository, ITimelinesService timelinesService)
    : ICommandHandler<UpdateNodeCommand, UpdateNodeResult>
{
    public async Task<UpdateNodeResult> Handle(UpdateNodeCommand command, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(command.Id, cancellationToken);

        if (node is null)
            throw new NodeNotFoundException(command.Id.ToString());

        node.Title = command.Title ?? node.Title;
        node.Description = command.Description ?? node.Description;
        node.Timestamp = command.Timestamp ?? node.Timestamp;
        node.Importance = command.Importance ?? node.Importance;
        node.Categories = command.Categories ?? node.Categories;
        node.Tags = command.Tags ?? node.Tags;

        var timeline = await timelinesService.GetTimelineByIdAsync(command.TimelineId ?? node.TimelineId, cancellationToken);

        if (timeline.Id is null)
            throw new NotFoundException($"Related timeline with ID {command.TimelineId ?? node.TimelineId} not found");
        
        node.TimelineId = TimelineId.Of(Guid.Parse(timeline.Id));
        
        await nodesRepository.UpdateNodeAsync(node, cancellationToken);

        return new UpdateNodeResult(node.ToNodeBaseDto());
    }
}
