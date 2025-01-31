using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

namespace Reminders.Application.Data.Abstractions;

public interface IRemindersRepository
{
    Task<Reminder> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken);
}

