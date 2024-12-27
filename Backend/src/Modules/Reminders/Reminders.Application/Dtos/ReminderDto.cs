namespace Reminders.Application.Dtos;

public record ReminderDto(
    string Id,
    string Title,
    string Description,
    DateTime DueDateTime,
    int Priority,
    DateTime NotificationTime,
    string Status);
