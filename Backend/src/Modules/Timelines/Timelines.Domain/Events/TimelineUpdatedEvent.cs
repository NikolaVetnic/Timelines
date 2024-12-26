using BuildingBlocks.Domain.Abstractions;
using Timelines.Domain.Models;

namespace Timelines.Domain.Events;

public record TimelineUpdatedEvent(Timeline Timeline) : IDomainEvent;
