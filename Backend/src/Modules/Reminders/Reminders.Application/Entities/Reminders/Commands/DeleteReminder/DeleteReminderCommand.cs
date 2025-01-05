namespace Reminders.Application.Entities.Reminders.Commands.DeleteReminder;

public record DeleteReminderCommand(string ReminderId) : ICommand<DeleteReminderResult>;

public record DeleteReminderResult(bool ReminderDeleted);

public class DeleteReminderCommandValidator : AbstractValidator<DeleteReminderCommand>
{
    public DeleteReminderCommandValidator()
    {
        RuleFor(x => x.ReminderId).NotEmpty().WithMessage("ReminderId is required");
    }
}
