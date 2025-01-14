using Reminders.Application.Entities.Reminders.Exceptions;
using Reminders.Application.Entities.Reminders.Extensions;

namespace Reminders.Application.Entities.Reminders.Queries.GetReminderById;

internal class GetReminderByIdHandler(IRemindersDbContext dbContext) : IQueryHandler<GetReminderByIdQuery, GetReminderByIdResult>
{
    public async Task<GetReminderByIdResult> Handle(GetReminderByIdQuery query, CancellationToken cancellationToken)
    {
        var reminderId = query.Id.ToString();

        var reminder = await dbContext.Reminders
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == ReminderId.Of(Guid.Parse(reminderId)), cancellationToken);

        if (reminder is null)
            throw new ReminderNotFoundException(reminderId);

        return new GetReminderByIdResult(reminder.ToReminderDto());
    }
}
