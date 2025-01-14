namespace Reminders.Application.Entities.Reminders.Commands.DeleteReminder;

public record DeleteReminderCommand(string Id) : ICommand<DeleteReminderResult>;

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
