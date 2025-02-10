using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface ITimelinesService
{
    Task<TimelineDto> GetTimelineByIdAsync(TimelineId timelineId, CancellationToken cancellationToken);
    Task<TimelineBaseDto> GetTimelineBaseDtoAsync(TimelineId timelineId, CancellationToken cancellationToken);
    Task AddNode(TimelineId timelineId, NodeId nodeId, CancellationToken cancellationToken);
}
