// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

using Core.Api.Sdk.Contracts.Reminders.Dtos;

namespace Core.Api.Sdk.Contracts.Reminders.Commands;

public class CreateReminderRequest
{
    public required ReminderDto Reminder { get; init; }
}
