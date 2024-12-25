using BuildingBlocks.Domain.Abstractions;

namespace Files.Domain.Events;

public record FileUpdatedEvent(Models.File File) : IDomainEvent;
