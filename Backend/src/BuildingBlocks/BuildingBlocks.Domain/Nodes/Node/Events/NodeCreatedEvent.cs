using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.ValueObjects.Ids;

namespace BuildingBlocks.Domain.Nodes.Node.Events;

public record NodeCreatedEvent(NodeId NodeId) : IDomainEvent;
