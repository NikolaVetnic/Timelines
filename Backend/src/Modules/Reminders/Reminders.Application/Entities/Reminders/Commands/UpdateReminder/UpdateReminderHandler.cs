using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using Reminders.Application.Data.Abstractions;
using Reminders.Application.Entities.Reminders.Exceptions;
using Reminders.Application.Entities.Reminders.Extensions;

namespace Reminders.Application.Entities.Reminders.Commands.UpdateReminder;

internal class UpdateReminderHandler(IRemindersRepository remindersRepository, INodesService nodesService)
    : ICommandHandler<UpdateReminderCommand, UpdateReminderResult>
{
    public async Task<UpdateReminderResult> Handle(UpdateReminderCommand command, CancellationToken cancellationToken)
    {
        var reminder = await remindersRepository.GetReminderByIdAsync(command.Id, cancellationToken);

        if (reminder is null)
            throw new ReminderNotFoundException(command.Id.ToString());

        reminder.Title = command.Title ?? reminder.Title;
        reminder.Description = command.Description ?? reminder.Description;
        reminder.NotifyAt = command.NotifyAt ?? reminder.NotifyAt;
        reminder.Priority = command.Priority ?? reminder.Priority;

        var node = await nodesService.GetNodeByIdAsync(command.NodeId ?? reminder.NodeId, cancellationToken);

        if (node.Id is null)
            throw new NotFoundException($"Related node with ID {command.NodeId ?? reminder.NodeId} not found");

        reminder.NodeId = NodeId.Of(Guid.Parse(node.Id));

        await remindersRepository.UpdateReminderAsync(reminder, cancellationToken);

        return new UpdateReminderResult(reminder.ToReminderDto(node));
    }
}
