using BuildingBlocks.Application.Data;
using Reminders.Application.Entities.Reminders.Exceptions;

namespace Reminders.Application.Entities.Reminders.Commands.DeleteReminder;

public class DeleteReminderHandler(IRemindersService remindersService) : ICommandHandler<DeleteReminderCommand, DeleteReminderResult>
{
    public async Task<DeleteReminderResult> Handle(DeleteReminderCommand command, CancellationToken cancellationToken)
    {
        var reminder = await remindersService.GetReminderBaseByIdAsync(command.Id, cancellationToken);

        if (reminder is null)
            throw new ReminderNotFoundException(command.Id.ToString());

        await remindersService.DeleteReminder(command.Id, cancellationToken);

        return new DeleteReminderResult(true);
    }
}
