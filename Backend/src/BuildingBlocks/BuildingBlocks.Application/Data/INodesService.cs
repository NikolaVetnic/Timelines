using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.ValueObjects;
using BuildingBlocks.Domain.ValueObjects.Ids;

namespace BuildingBlocks.Application.Data;

public interface INodesService
{
    Task<NodeDto> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task<NodeBaseDto> GetNodeBaseByIdAsync(NodeId nodeId, CancellationToken cancellationToken);
    Task AddReminder(NodeId nodeId, ReminderId reminderId, CancellationToken cancellationToken);
}
