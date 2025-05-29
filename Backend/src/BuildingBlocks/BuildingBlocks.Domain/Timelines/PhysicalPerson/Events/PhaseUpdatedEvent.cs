using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;

namespace BuildingBlocks.Domain.Timelines.PhysicalPerson.Events;

public record PhysicalPersonUpdatedEvent(PhysicalPersonId PhysicalPersonId) : IDomainEvent;
