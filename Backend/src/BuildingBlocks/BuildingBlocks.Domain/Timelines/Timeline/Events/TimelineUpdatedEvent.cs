using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.ValueObjects.Ids;

namespace BuildingBlocks.Domain.Timelines.Timeline.Events;

public record TimelineUpdatedEvent(TimelineId TimelineId) : IDomainEvent;
