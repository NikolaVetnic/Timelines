namespace Reminders.Application.Entities.Reminders.Dtos;

public record ReminderDto(
    string Id,
    string Title,
    string Description,
    DateTime DueDateTime,
    int Priority,
    DateTime NotificationTime,
    string Status);
