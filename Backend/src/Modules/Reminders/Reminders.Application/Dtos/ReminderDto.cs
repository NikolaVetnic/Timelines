namespace Reminders.Application.Dtos;

public record ReminderDto(
    string Title,
    string Description,
    DateTime DueDateTime,
    string Priority,
    DateTime NotificationTime,
    string Status);
