using Nodes.Domain.Models;

namespace Nodes.Domain.Events;

public record NodeCreatedEvent(Node Node) : IDomainEvent;
