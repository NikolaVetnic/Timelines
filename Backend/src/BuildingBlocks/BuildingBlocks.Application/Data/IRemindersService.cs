using BuildingBlocks.Domain.Reminders.Dtos;
using BuildingBlocks.Domain.Reminders.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface IRemindersService
{
    Task<ReminderDto> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken);
    Task<ReminderBaseDto> GetReminderBaseByIdAsync(ReminderId reminderId, CancellationToken cancellationToken);
}
