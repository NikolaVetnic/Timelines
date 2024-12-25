using Files.Domain.Models;

namespace Files.Domain.Events;

public record FileUpdatedEvent(FileAsset FileAsset) : IDomainEvent;
