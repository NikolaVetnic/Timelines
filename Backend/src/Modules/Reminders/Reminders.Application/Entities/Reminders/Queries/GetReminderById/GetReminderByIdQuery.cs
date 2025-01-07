// ReSharper disable ClassNeverInstantiated.Global

namespace Reminders.Application.Entities.Reminders.Queries.GetReminderById;

public record GetReminderByIdQuery(string Id) : IQuery<GetReminderByIdResult>;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record GetReminderByIdResult(ReminderDto ReminderDto);
