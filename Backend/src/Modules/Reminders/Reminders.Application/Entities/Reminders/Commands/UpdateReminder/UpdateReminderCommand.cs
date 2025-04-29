using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

namespace Reminders.Application.Entities.Reminders.Commands.UpdateReminder;

// ReSharper disable once ClassNeverInstantiated.Global
public class UpdateReminderCommand : ICommand<UpdateReminderResult>
{
    public required ReminderId Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? NotifyAt { get; set; }
    public int? Priority { get; set; }
    public NodeId? NodeId { get; set; }
}

// ReSharper disable once NotAccessedPositionalProperty.Global
public record UpdateReminderResult(ReminderDto ReminderDto);

public class UpdateReminderCommandValidator : AbstractValidator<UpdateReminderCommand>
{
    public UpdateReminderCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Title));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.NotifyAt)
            .GreaterThan(DateTime.Now).WithMessage("Notify at must be in the future.")
            .When(x => x.NotifyAt is not null);
    }
}
