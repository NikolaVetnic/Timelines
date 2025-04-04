﻿using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface ITimelinesService
{
    Task<List<TimelineDto>> ListTimelinesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<TimelineDto> GetTimelineByIdAsync(TimelineId timelineId, CancellationToken cancellationToken);
    Task<TimelineBaseDto> GetTimelineBaseByIdAsync(TimelineId timelineId, CancellationToken cancellationToken);
    Task<long> CountTimelinesAsync(CancellationToken cancellationToken);

    Task AddNode(TimelineId timelineId, NodeId nodeId, CancellationToken cancellationToken);
    Task RemoveNode(TimelineId timelineId, NodeId nodeId, CancellationToken cancellationToken);
    Task RemoveNodes(TimelineId timelineId, IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
    
    Task<List<TimelineBaseDto>> GetTimelinesBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
}
