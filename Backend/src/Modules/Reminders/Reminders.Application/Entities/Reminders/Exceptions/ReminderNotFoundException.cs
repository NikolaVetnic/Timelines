namespace Reminders.Application.Entities.Reminders.Exceptions;

public class ReminderNotFoundException(string id) : NotFoundException("Reminder", id);
