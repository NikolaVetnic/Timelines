using Reminders.Application.Entities.Reminders.Exceptions;

namespace Reminders.Application.Entities.Reminders.Commands.UpdateReminder;

public class UpdateOrderHandler(IRemindersDbContext dbContext) : ICommandHandler<UpdateReminderCommand, UpdateReminderResult>
{
    public async Task<UpdateReminderResult> Handle(UpdateReminderCommand command, CancellationToken cancellationToken)
    {
        var reminder = await dbContext.Reminders
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == ReminderId.Of(Guid.Parse(command.Reminder.Id)), cancellationToken);

        if (reminder is null)
            throw new ReminderNotFoundException(command.Reminder.Id);

        UpdateOrderWithNewValues(reminder, command.Reminder);

        dbContext.Reminders.Update(reminder);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateReminderResult(true);
    }

    private static void UpdateOrderWithNewValues(Reminder reminder, ReminderDto reminderDto)
    {
        reminder.Update(
            reminderDto.Title,
            reminderDto.Description,
            reminderDto.DueDateTime,
            reminderDto.Priority,
            reminderDto.NotificationTime,
            reminderDto.Status);
    }
}
