using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.ValueObjects.Ids;

namespace BuildingBlocks.Domain.Files.File.Events;

public record FileAssetUpdatedEvent(FileAssetId FileAssetId) : IDomainEvent;
