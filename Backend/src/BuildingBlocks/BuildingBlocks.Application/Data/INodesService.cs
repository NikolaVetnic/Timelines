using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface INodesService
{
    Task<NodeDto> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task<NodeBaseDto> GetNodeBaseByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task AddReminder(NodeId nodeId, ReminderId reminderId, CancellationToken cancellationToken);
    Task RemoveNode(NodeId nodeId, CancellationToken cancellationToken);
    Task<List<NodeBaseDto>> GetNodeRangeByIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
    Task<List<NodeBaseDto>> GetNodesBaseBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken);
}
