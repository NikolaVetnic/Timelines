using Reminders.Application.Entities.Reminders.Exceptions;

namespace Reminders.Application.Entities.Reminders.Commands.DeleteReminder;

public class DeleteReminderHandler(IRemindersDbContext dbContext) : ICommandHandler<DeleteReminderCommand, DeleteReminderResult>
{
    public async Task<DeleteReminderResult> Handle(DeleteReminderCommand command, CancellationToken cancellationToken)
    {
        var reminder = await dbContext.Reminders
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == ReminderId.Of(Guid.Parse(command.Id)), cancellationToken);

        if (reminder is null)
            throw new ReminderNotFoundException(command.Id);

        dbContext.Reminders.Remove(reminder);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteReminderResult(true);
    }
}
