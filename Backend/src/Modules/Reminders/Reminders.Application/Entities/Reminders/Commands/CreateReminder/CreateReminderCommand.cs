using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

namespace Reminders.Application.Entities.Reminders.Commands.CreateReminder;

// ReSharper disable once ClassNeverInstantiated.Global
public class CreateReminderCommand : ICommand<CreateReminderResult>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime NotifyAt { get; set; }
    public required int Priority { get; set; }
    public required NodeId NodeId { get; set; }
}

// ReSharper disable once NotAccessedPositionalProperty.Global
public record CreateReminderResult(ReminderId Id);

public class CreateReminderCommandValidator : AbstractValidator<CreateReminderCommand>
{
    public CreateReminderCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.NotifyAt)
            .GreaterThan(DateTime.Now).WithMessage("Notify at must be in the future.");

        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required.");
        
        RuleFor(x => x.NodeId)
            .NotEmpty().WithMessage("NodeId is required.");
    }
}
