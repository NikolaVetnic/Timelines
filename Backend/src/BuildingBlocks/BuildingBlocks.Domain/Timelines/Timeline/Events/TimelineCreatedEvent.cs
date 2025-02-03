using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace BuildingBlocks.Domain.Timelines.Timeline.Events;

public record TimelineCreatedEvent(TimelineId TimelineId) : IDomainEvent;
