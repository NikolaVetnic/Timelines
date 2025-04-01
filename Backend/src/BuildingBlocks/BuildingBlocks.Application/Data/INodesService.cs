using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface INodesService
{
    Task<List<NodeDto>> ListNodesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<NodeBaseDto>> GetNodesByIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
    Task<long> CountNodesAsync(CancellationToken cancellationToken);
    
    Task<NodeDto> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task<NodeBaseDto> GetNodeBaseByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    
    Task AddReminder(NodeId nodeId, ReminderId reminderId, CancellationToken cancellationToken);
    Task AddNote(NodeId nodeId, NoteId noteId, CancellationToken cancellationToken);

    Task DeleteNode(NodeId nodeId, CancellationToken cancellationToken);
    Task DeleteNodes(TimelineId timelineId, IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
    Task RemoveNote(NodeId nodeId, NoteId noteId, CancellationToken cancellationToken);

    Task<List<NodeBaseDto>> GetNodesBaseBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken);

}
