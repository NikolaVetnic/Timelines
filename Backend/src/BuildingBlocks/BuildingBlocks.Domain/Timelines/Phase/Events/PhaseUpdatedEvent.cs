using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;

namespace BuildingBlocks.Domain.Timelines.Phase.Events;

public record PhaseUpdatedEvent(PhaseId PhaseId) : IDomainEvent;
