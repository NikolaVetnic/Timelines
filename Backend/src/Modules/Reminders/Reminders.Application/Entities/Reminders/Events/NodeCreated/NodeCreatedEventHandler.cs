using BuildingBlocks.Domain.Nodes.Node.Events;
using MediatR;

namespace Reminders.Application.Entities.Reminders.Events.NodeCreated;

public class NodeCreatedEventHandler : INotificationHandler<NodeCreatedEvent>
{
    public Task Handle(NodeCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"NodeCreatedEventHandler : {notification.NodeId}");
        return Task.CompletedTask;
    }
}
