using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface IRemindersService
{
    Task<ReminderDto> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken);
    Task<ReminderBaseDto> GetReminderBaseByIdAsync(ReminderId reminderId, CancellationToken cancellationToken);
}
