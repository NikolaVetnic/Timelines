using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

namespace Reminders.Application.Entities.Reminders.Commands.ReviveReminder;

public record ReviveReminderCommand(ReminderId Id) : ICommand<ReviveReminderResult>
{
    public ReviveReminderCommand(string Id) : this(ReminderId.Of(Guid.Parse(Id))) { }
}

public record ReviveReminderResult(bool ReminderRevived);

public class ReviveReminderCommandValidator : AbstractValidator<ReviveReminderCommand>
{
    public ReviveReminderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
