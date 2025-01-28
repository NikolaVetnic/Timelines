using BuildingBlocks.Domain.Nodes.Node.Events;
using MediatR;

namespace Reminders.Application.Entities.Reminders.Events.NodeUpdated;

public class NodeUpdatedEventHandler : INotificationHandler<NodeUpdatedEvent>
{
    public Task Handle(NodeUpdatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"NodeUpdatedEventHandler : {notification.NodeId}");
        return Task.CompletedTask;
    }
}
