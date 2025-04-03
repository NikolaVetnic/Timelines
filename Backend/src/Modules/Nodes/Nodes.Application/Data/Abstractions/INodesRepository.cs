using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Nodes.Application.Data.Abstractions;

public interface INodesRepository
{
    Task<List<Node>> ListNodesPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<long> NodeCountAsync(CancellationToken cancellationToken);
    
    Task<Node> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task<List<Node>> GetNodesByIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
    
    Task UpdateNodeAsync(Node node, CancellationToken cancellationToken);
    Task DeleteNode(NodeId nodeId, CancellationToken cancellationToken);
    Task DeleteNodes(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
    
    Task<IEnumerable<Node>> GetNodesBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken);
    Task<IEnumerable<Node>> GetNodesBelongingToNotesIdsAsync(IEnumerable<NoteId> noteIds, CancellationToken cancellationToken);
}
