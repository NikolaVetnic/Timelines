using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Mapster;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Timelines.Extensions;

namespace Timelines.Application.Data;

public class TimelinesService(ITimelinesRepository timelinesRepository, INodesService nodesService) : ITimelinesService
{
    public async Task<List<TimelineDto>> ListTimelinesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var timelines = await timelinesRepository.ListTimelinessPaginatedAsync(pageIndex, pageSize, cancellationToken);

        var nodes = await nodesService.GetNodesBaseBelongingToTimelineIdsAsync(timelines.Select(t => t.Id), cancellationToken);

        var timelineDtos = timelines.Select(t =>
                t.ToTimelineDto(
                    nodes
                        .Where(n => t.NodeIds.Select(id => id.ToString()).Contains(n.Id))
                        .Select(n => new NodeBaseDto(
                            id: n.Id!.ToString(),
                            title: n.Title,
                            description: n.Description,
                            phase: n.Phase,
                            timestamp: n.Timestamp,
                            importance: n.Importance,
                            categories: n.Categories,
                            tags: n.Tags)
                        )
                        .ToList()
                ))
            .ToList();

        return timelineDtos;
    }

    public async Task<TimelineDto> GetTimelineByIdAsync(TimelineId timelineId, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTimelineByIdAsync(timelineId, cancellationToken);

        var nodes = await nodesService.GetNodeRangeByIdsAsync(timeline.NodeIds, cancellationToken);

        return timeline.ToTimelineDtoWith(nodes);
    }

    public async Task<TimelineBaseDto> GetTimelineBaseDtoAsync(TimelineId timelineId, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTimelineByIdAsync(timelineId, cancellationToken);
        var timelineBaseDto = timeline.Adapt<TimelineBaseDto>();

        return timelineBaseDto;
    }

    public async Task<long> CountTimelinesAsync(CancellationToken cancellationToken)
    {
        return await timelinesRepository.TimelineCountAsync(cancellationToken);
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
