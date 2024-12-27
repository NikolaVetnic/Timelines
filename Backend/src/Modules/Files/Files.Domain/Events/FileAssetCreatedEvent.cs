using Files.Domain.Models;

namespace Files.Domain.Events;

public record FileAssetCreatedEvent(FileAsset FileAsset) : IDomainEvent;
