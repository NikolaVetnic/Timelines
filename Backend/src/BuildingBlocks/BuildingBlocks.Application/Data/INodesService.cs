using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface INodesService
{
    Task<List<NodeDto>> ListNodesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<NodeBaseDto>> ListNodesByTimelineIdPaginated(TimelineId timelineId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<NodeBaseDto>> ListNodesBelongingToPhasePaginated(DateTime startDate, DateTime? endDate, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<NodeBaseDto>> GetNodesByIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
    Task<long> CountNodesAsync(CancellationToken cancellationToken);
    Task<long> CountNodesByTimelineIdAsync(TimelineId timelineId, CancellationToken cancellationToken);
    Task<long> CountNodesBelongingToPhase(DateTime startDate, DateTime? endDate, CancellationToken cancellationToken);

    Task<NodeDto> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task<NodeBaseDto> GetNodeBaseByIdAsync(NodeId nodeId, CancellationToken cancellationToken);

    Task<NodeId> CloneNodeIntoTimelineAsync(NodeId nodeTemplateId, TimelineId timelineId, CancellationToken cancellationToken);
    
    Task AddReminder(NodeId nodeId, ReminderId reminderId, CancellationToken cancellationToken);
    Task RemoveReminder(NodeId nodeId, ReminderId reminderId, CancellationToken cancellationToken);
    Task RemoveReminders(NodeId nodeId, IEnumerable<ReminderId> reminderIds, CancellationToken cancellationToken);

    Task AddNote(NodeId nodeId, NoteId noteId, CancellationToken cancellationToken);
    Task RemoveNote(NodeId nodeId, NoteId noteId, CancellationToken cancellationToken);
    Task RemoveNotes(NodeId nodeId, IEnumerable<NoteId> noteIds, CancellationToken cancellationToken);

    Task DeleteNode(NodeId nodeId, CancellationToken cancellationToken);
    Task DeleteNodes(TimelineId timelineId, IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);

    Task<List<NodeBaseDto>> GetNodesBaseBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken);

    Task AddFileAsset(NodeId nodeId, FileAssetId fileAssetId, CancellationToken cancellationToken);
    Task RemoveFileAsset(NodeId nodeId, FileAssetId fileAssetId, CancellationToken cancellationToken);
    Task RemoveFileAssets(NodeId nodeId, IEnumerable<FileAssetId> fileAssetIds, CancellationToken cancellationToken);
}
