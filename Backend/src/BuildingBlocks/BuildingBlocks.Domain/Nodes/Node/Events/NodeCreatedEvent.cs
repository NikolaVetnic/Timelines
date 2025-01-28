using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace BuildingBlocks.Domain.Nodes.Node.Events;

public record NodeCreatedEvent(NodeId NodeId) : IDomainEvent;
