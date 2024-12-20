using BuildingBlocks.Application.Cqrs;
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
        RuleFor(x => x.Reminder.Title).NotEmpty().WithMessage("Title is required.");

        // ToDo: Add remaining Node command validators
    }
}
