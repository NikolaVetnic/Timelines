// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

using Reminders.Domain.Models;

namespace Core.Api.Sdk.Contracts.Reminders.Dtos;

public class ReminderDto
{
    public string? Id { get; set; }
    public required string Title { get; init;  } 
    public required string Description { get; init; }
    public required DateTime NotifyAt { get; init; } 
    public required int Priority { get; init; }
    public required List<Reminder> RelatedReminders { get; init; }
}
