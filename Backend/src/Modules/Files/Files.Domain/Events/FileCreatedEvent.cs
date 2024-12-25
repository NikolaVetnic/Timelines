using BuildingBlocks.Domain.Abstractions;

namespace Files.Domain.Events;

public record FileCreatedEvent(Models.File File) : IDomainEvent;
