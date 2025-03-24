using BuildingBlocks.Application.Data;
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
        var timelinesDtos = timelines.ToTimelineDtoList().ToList();

        // todo: super dirty and terrible, needs lots of improvement
        foreach (var timelineDto in timelinesDtos)
        {
            var timelineId = TimelineId.Of(Guid.Parse(timelineDto.Id ?? string.Empty));

            foreach (var nodeId in timelines.First(n => Equals(n.Id, timelineId)).NodeIds)
            {
                var reminder = await nodesService.GetNodeBaseByIdAsync(nodeId, cancellationToken);
                timelineDto.Nodes.Add(reminder);
            }
        }

        return timelinesDtos;
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
