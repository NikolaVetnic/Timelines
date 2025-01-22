// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

using Core.Api.Sdk.Contracts.Reminders.ValueObjects;

namespace Core.Api.Sdk.Contracts.Reminders.Commands;

public class CreateReminderResponse
{
    public required ReminderId Id { get; init; }
}
