using BuildingBlocks.Application.Data;
using Reminders.Application.Data.Abstractions;
using Reminders.Application.Entities.Reminders.Exceptions;

namespace Reminders.Application.Entities.Reminders.Commands.ReviveReminder;

internal class ReviveReminderHandler(IRemindersRepository remindersRepository) : ICommandHandler<ReviveReminderCommand, ReviveReminderResult>
{
    public async Task<ReviveReminderResult> Handle(ReviveReminderCommand command, CancellationToken cancellationToken)
    {
        var reminder = await remindersRepository.GetFlaggedForDeletionReminderByIdAsync(command.Id, cancellationToken);

        if (reminder is null)
            throw new ReminderNotFoundException(command.Id.ToString());

        await remindersRepository.ReviveReminder(command.Id, cancellationToken);

        return new ReviveReminderResult(true);
    }
}
