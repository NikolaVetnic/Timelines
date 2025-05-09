using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using Reminders.Application.Data.Abstractions;

namespace Reminders.Application.Entities.Reminders.Commands.CreateReminder;

internal class CreateReminderHandler(ICurrentUser currentUser, IRemindersDbContext dbContext, INodesService nodesService)
    : ICommandHandler<CreateReminderCommand, CreateReminderResult>
{
    public async Task<CreateReminderResult> Handle(CreateReminderCommand command, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId!;
        var reminder = command.ToReminder(userId);

        dbContext.Reminders.Add(reminder);
        await dbContext.SaveChangesAsync(cancellationToken);

        await nodesService.AddReminder(reminder.NodeId, reminder.Id, cancellationToken);

        return new CreateReminderResult(reminder.Id);
    }
}

internal static class CreateReminderCommandExtensions
{
    public static Reminder ToReminder(this CreateReminderCommand command, string userId)
    {
        return Reminder.Create(
            ReminderId.Of(Guid.NewGuid()),
            command.Title,
            command.Description,
            command.NotifyAt,
            command.Priority,
            userId,
            command.NodeId
        );
    }
}
