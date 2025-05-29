using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;

namespace BuildingBlocks.Domain.Timelines.PhysicalPerson.Events;

public record PhysicalPersonCreatedEvent(PhysicalPersonId PhysicalPersonId) : IDomainEvent;
