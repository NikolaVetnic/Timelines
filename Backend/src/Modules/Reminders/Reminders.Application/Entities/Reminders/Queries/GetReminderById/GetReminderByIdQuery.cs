// ReSharper disable ClassNeverInstantiated.Global

using Reminders.Application.Entities.Reminders.Dtos;

namespace Reminders.Application.Entities.Reminders.Queries.GetReminderById;

public record GetReminderByIdQuery(ReminderId Id) : IQuery<GetReminderByIdResult>
{
    public GetReminderByIdQuery(string Id) : this(ReminderId.Of(Guid.Parse(Id))) { }
}

// ReSharper disable once NotAccessedPositionalProperty.Global

public record GetReminderByIdResult(ReminderDto ReminderDto);

public class GetReminderByIdQueryValidator : AbstractValidator<GetReminderByIdQuery>
{
    public GetReminderByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
