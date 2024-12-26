using Reminders.Domain.Models;

namespace Reminders.Domain.Events;

public record ReminderUpdatedEvent(Reminder Reminder) : IDomainEvent;
