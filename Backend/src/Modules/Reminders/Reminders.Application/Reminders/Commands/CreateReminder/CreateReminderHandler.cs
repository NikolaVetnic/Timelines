using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.ValueObjects.Ids;
using Reminders.Application.Data;
using Reminders.Domain.Models;

namespace Reminders.Application.Reminders.Commands.CreateReminder;

public class CreateReminderHandler(IRemindersDbContext dbContext) :
    ICommandHandler<CreateReminderCommand, CreateReminderResult>
{
    public async Task<CreateReminderResult> Handle(CreateReminderCommand command, CancellationToken cancellationToken)
    {
        var reminder = Reminder.Create(
            ReminderId.Of(Guid.NewGuid()),
            command.Reminder.Title
        );

        dbContext.Reminders.Add(reminder);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateReminderResult(reminder.Id);
    }
}
