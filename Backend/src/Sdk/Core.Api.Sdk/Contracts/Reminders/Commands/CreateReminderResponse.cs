// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

using BuildingBlocks.Domain.ValueObjects.Ids;

namespace Core.Api.Sdk.Contracts.Reminders.Commands;

public class CreateReminderResponse
{
    public required ReminderId Id { get; init; }
}
