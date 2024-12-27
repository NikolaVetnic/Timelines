using Files.Domain.Models;

namespace Files.Domain.Events;

public record FileAssetUpdatedEvent(FileAsset FileAsset) : IDomainEvent;
