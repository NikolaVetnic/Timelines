using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using System.Text.Json.Serialization;

namespace Reminders.Api.Controllers.Reminders;

public record CreateReminderResponse(ReminderId Id);

public record GetReminderByIdResponse([property: JsonPropertyName("reminder")] ReminderDto ReminderDto);

public record ListRemindersResponse(PaginatedResult<ReminderDto> Reminders);

public record UpdateReminderResponse(ReminderDto Reminder);

public record DeleteReminderResponse(bool ReminderDeleted);

public record ReviveReminderResponse(bool ReminderRevived);
