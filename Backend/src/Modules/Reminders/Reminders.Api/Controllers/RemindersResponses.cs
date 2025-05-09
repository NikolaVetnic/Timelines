using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using System.Text.Json.Serialization;

namespace Reminders.Api.Controllers;

public record CreateReminderResponse(ReminderId Id);

public record GetReminderByIdResponse([property: JsonPropertyName("reminder")] ReminderDto ReminderDto);
