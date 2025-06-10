using BuildingBlocks.Application.Data;
using Reminders.Application.Entities.Reminders.Exceptions;

namespace Reminders.Application.Entities.Reminders.Commands.ReviveReminder;

internal class ReviveReminderHandler(IRemindersService remindersService) : ICommandHandler<ReviveReminderCommand, ReviveReminderResult>
{
    public async Task<ReviveReminderResult> Handle(ReviveReminderCommand command, CancellationToken cancellationToken)
    {
        var reminder = await remindersService.GetReminderBaseByIdAsync(command.Id, cancellationToken);

        if (reminder is null)
            throw new ReminderNotFoundException(command.Id.ToString());

        await remindersService.ReviveReminder(command.Id, cancellationToken);

        return new ReviveReminderResult(true);
    }
}
