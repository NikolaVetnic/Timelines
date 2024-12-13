using Nodes.Domain.Models;

namespace Nodes.Domain.Events;

public record NodeUpdatedEvent(Node node) : IDomainEvent { }