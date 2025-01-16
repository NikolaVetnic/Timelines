// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

using Core.Api.Sdk.Contracts.Reminders.Dtos;

namespace Core.Api.Sdk.Contracts.Reminders.Queries;

public class GetReminderByIdResponse
{
    public required ReminderDto ReminderDto { get; init; }
}
