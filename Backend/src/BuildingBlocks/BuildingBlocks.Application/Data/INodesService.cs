using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface INodesService
{
    Task<List<NodeDto>> ListNodesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<NodeDto> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task<NodeBaseDto> GetNodeBaseByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task<long> CountNodesAsync(CancellationToken cancellationToken);
    Task AddReminder(NodeId nodeId, ReminderId reminderId, CancellationToken cancellationToken);
    Task AddNote(NodeId nodeId, NoteId noteId, CancellationToken cancellationToken);
    Task RemoveNote(NodeId nodeId, NoteId noteId, CancellationToken cancellationToken);
}
