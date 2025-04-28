using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Files.File.ValueObjects;

namespace BuildingBlocks.Domain.Files.File.Events;

public record FileAssetUpdatedEvent(FileAssetId FileAssetId) : IDomainEvent;
