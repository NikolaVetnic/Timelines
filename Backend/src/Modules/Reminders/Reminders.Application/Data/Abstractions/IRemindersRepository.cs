using BuildingBlocks.Domain.Reminders.ValueObjects;

namespace Reminders.Application.Data.Abstractions;

public interface IRemindersRepository
{
    Task<Reminder> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken);
}

