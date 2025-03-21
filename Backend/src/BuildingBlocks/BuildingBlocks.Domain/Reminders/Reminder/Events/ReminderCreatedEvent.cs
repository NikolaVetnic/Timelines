﻿using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

namespace BuildingBlocks.Domain.Reminders.Reminder.Events;

public record ReminderCreatedEvent(ReminderId ReminderId) : IDomainEvent;
