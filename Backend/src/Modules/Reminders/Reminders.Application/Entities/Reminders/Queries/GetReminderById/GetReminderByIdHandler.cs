using Reminders.Application.Entities.Reminders.Exceptions;
using Reminders.Application.Entities.Reminders.Extensions;

namespace Reminders.Application.Entities.Reminders.Queries.GetReminderById;

internal class GetReminderByIdHandler(IRemindersDbContext dbContext) : IQueryHandler<GetReminderByIdQuery, GetReminderByIdResult>
{
    public async Task<GetReminderByIdResult> Handle(GetReminderByIdQuery request, CancellationToken cancellationToken)
    {
        var reminder = await dbContext.Reminders
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == ReminderId.Of(Guid.Parse(request.Id)), cancellationToken);

        if (reminder is null)
            throw new ReminderNotFoundException(request.Id);

        return new GetReminderByIdResult(reminder.ToReminderDto());
    }
}
