using Reminders.Application.Entities.Reminders.Dtos;

namespace Reminders.Application.Entities.Reminders.Queries.GetReminderById;

public record GetReminderByIdQuery(string Id) : IQuery<GetReminderByIdResult>;

public record GetReminderByIdResult(ReminderDto ReminderDto);
