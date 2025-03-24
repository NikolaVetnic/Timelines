using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Timelines.Extensions;

namespace Timelines.Application.Data;

public class TimelinesService(ITimelinesRepository timelinesRepository, IServiceProvider serviceProvider) : ITimelinesService
{
    public async Task<TimelineDto> GetTimelineByIdAsync(TimelineId timelineId, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTimelineByIdAsync(timelineId, cancellationToken);
        var nodesService = serviceProvider.GetRequiredService<INodesService>();

        var nodes = await nodesService.GetNodeRangeByIdsAsync(timeline.NodeIds, cancellationToken);

        return timeline.ToTimelineDtoWith(nodes);
    }

    public async Task<TimelineBaseDto> GetTimelineBaseDtoAsync(TimelineId timelineId, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTimelineByIdAsync(timelineId, cancellationToken);
        var timelineBaseDto = timeline.Adapt<TimelineBaseDto>();

        return timelineBaseDto;
    }

    public async Task AddNode(TimelineId timelineId, NodeId nodeId, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTimelineByIdAsync(timelineId, cancellationToken);
        timeline.AddNode(nodeId);

        await timelinesRepository.UpdateTimelineAsync(timeline, cancellationToken);
    }

    public async Task RemoveNode(TimelineId timelineId, NodeId nodeId, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTimelineByIdAsync(timelineId, cancellationToken);
        timeline.RemoveNode(nodeId);

        await timelinesRepository.UpdateTimelineAsync(timeline, cancellationToken);
    }
}
