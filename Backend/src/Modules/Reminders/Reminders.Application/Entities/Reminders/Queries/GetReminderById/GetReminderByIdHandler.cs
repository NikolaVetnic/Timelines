using BuildingBlocks.Application.Data;
using Reminders.Application.Entities.Reminders.Exceptions;

namespace Reminders.Application.Entities.Reminders.Queries.GetReminderById;

internal class GetReminderByIdHandler(IRemindersService remindersService) : IQueryHandler<GetReminderByIdQuery, GetReminderByIdResult>
{
    public async Task<GetReminderByIdResult> Handle(GetReminderByIdQuery query, CancellationToken cancellationToken)
    {
        var reminder = await remindersService.GetReminderByIdAsync(query.Id, cancellationToken);

        if (reminder is null)
            throw new ReminderNotFoundException(query.Id.ToString());

        return new GetReminderByIdResult(reminder);
    }
}
