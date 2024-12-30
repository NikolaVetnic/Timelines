namespace Reminders.Application.Entities.Reminders.Commands.CreateReminder;

internal class CreateReminderHandler(IRemindersDbContext dbContext) : ICommandHandler<CreateReminderCommand, CreateReminderResult>
{
    public async Task<CreateReminderResult> Handle(CreateReminderCommand command, CancellationToken cancellationToken)
    {
        var reminder = command.ToReminder();

        dbContext.Reminders.Add(reminder);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateReminderResult(reminder.Id);
    }
}

internal static class CreateReminderCommandExtensions
{
    public static Reminder ToReminder(this CreateReminderCommand command)
    {
        return Reminder.Create(
            ReminderId.Of(Guid.NewGuid()),
            command.Reminder.Title,
            command.Reminder.Description,
            command.Reminder.DueDateTime,
            command.Reminder.Priority,
            command.Reminder.NotificationTime,
            command.Reminder.Status
        );
    }
}
