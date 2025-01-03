using BuildingBlocks.Domain.ValueObjects.Ids;
using Reminders.Application.Data;

namespace Reminders.Application.Reminders.Commands.CreateReminder;

internal class CreateReminderHandler(IRemindersDbContext dbContext) :
    ICommandHandler<CreateReminderCommand, CreateReminderResult>
{
    public async Task<CreateReminderResult> Handle(CreateReminderCommand command, CancellationToken cancellationToken)
    {
        var reminder = Reminder.Create(
            ReminderId.Of(Guid.NewGuid()),
            command.Reminder.Title,
            command.Reminder.Description,
            command.Reminder.DueDateTime,
            command.Reminder.Priority,
            command.Reminder.NotificationTime,
            command.Reminder.Status
        );

        dbContext.Reminders.Add(reminder);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateReminderResult(reminder.Id);
    }
}
