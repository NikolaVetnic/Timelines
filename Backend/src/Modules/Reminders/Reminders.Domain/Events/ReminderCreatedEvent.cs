using Reminders.Domain.Models;

namespace Reminders.Domain.Events;

public record ReminderCreatedEvent(Reminder Reminder) : IDomainEvent { }
