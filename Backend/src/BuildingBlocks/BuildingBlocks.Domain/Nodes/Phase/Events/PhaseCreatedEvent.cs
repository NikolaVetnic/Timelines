using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;

namespace BuildingBlocks.Domain.Nodes.Phase.Events;

public record PhaseCreatedEvent(PhaseId PhaseId) : IDomainEvent;
