using Timelines.Domain.Models;

namespace Timelines.Domain.Events;

public record TimelineCreatedEvent(Timeline Timeline) : IDomainEvent;
