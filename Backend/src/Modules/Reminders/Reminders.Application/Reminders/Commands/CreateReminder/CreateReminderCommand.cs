using BuildingBlocks.Domain.ValueObjects.Ids;
using FluentValidation;
using Reminders.Application.Dtos;

namespace Reminders.Application.Reminders.Commands.CreateReminder;

public record CreateReminderCommand(ReminderDto Reminder) : ICommand<CreateReminderResult>;

public record CreateReminderResult(ReminderId Id);

public class CreateReminderCommandValidator : AbstractValidator<CreateReminderCommand>
{
    public CreateReminderCommandValidator()
    {
        RuleFor(x => x.Reminder.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Reminder.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Reminder.DueDateTime)
            .GreaterThan(DateTime.Now).WithMessage("Due date and time must be in the future.");

        RuleFor(x => x.Reminder.Priority)
            .NotEmpty().WithMessage("Priority is required.");

        RuleFor(x => x.Reminder.NotificationTime)
            .LessThan(x => x.Reminder.DueDateTime).WithMessage("Notification time must be before the due date and time.")
            .GreaterThan(DateTime.Now).WithMessage("Notification time must be in the future.");

        RuleFor(x => x.Reminder.Status)
            .NotEmpty().WithMessage("Status is required.");
    }
}
