using BuildingBlocks.Domain.Reminders.ValueObjects;

namespace Reminders.Application.Entities.Reminders.Commands.DeleteReminder;

public record DeleteReminderCommand(ReminderId Id) : ICommand<DeleteReminderResult>
{
    public DeleteReminderCommand(string Id) : this(ReminderId.Of(Guid.Parse(Id))) { }
}

public record DeleteReminderResult(bool ReminderDeleted);

public class DeleteReminderCommandValidator : AbstractValidator<DeleteReminderCommand>
{
    public DeleteReminderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
