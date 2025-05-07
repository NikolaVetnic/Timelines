using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Timelines.Exceptions;
using Timelines.Application.Entities.Timelines.Extensions;

namespace Timelines.Application.Entities.Timelines.Commands.CreateTimelineWithTemplate;

internal class CreateTimelineWithTemplateHandler(ICurrentUser currentUser, ITimelinesRepository timelinesRepository, INodesService nodesService)
    : ICommandHandler<CreateTimelineWithTemplateCommand, CreateTimelineWithTemplateResult>
{
    public async Task<CreateTimelineWithTemplateResult> Handle(CreateTimelineWithTemplateCommand command,
        CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId!;

        var template = await timelinesRepository
            .GetTimelineByIdAsync(command.Id, cancellationToken) ?? throw new TimelineNotFoundException(command.Id.ToString());

        var newTimeline = Timeline.Create(
            TimelineId.Of(Guid.NewGuid()),
            command.Title ?? template.Title,
            command.Description ?? template.Description,
            userId
        );

        var nodeTemplates = await nodesService
            .GetNodesBaseBelongingToTimelineIdsAsync(
                [template.Id], cancellationToken);

        var cloneTasks = nodeTemplates.Select(n =>
        {
            var nodeId = NodeId.Of(Guid.Parse(n.Id!));
            return nodesService.CloneNodeIntoTimelineAsync(
                nodeId,
                newTimeline.Id,
                cancellationToken);
        });

        var clonedIds = await Task.WhenAll(cloneTasks);

        foreach (var id in clonedIds)
            newTimeline.AddNode(id);

        await timelinesRepository.CreateTimelineAsync(newTimeline, cancellationToken);

        return new CreateTimelineWithTemplateResult(newTimeline.ToTimelineDto());
    }
}
