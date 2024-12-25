using Files.Domain.Models;

namespace Files.Domain.Events;

public record FileCreatedEvent(FileAsset FileAsset) : IDomainEvent;
