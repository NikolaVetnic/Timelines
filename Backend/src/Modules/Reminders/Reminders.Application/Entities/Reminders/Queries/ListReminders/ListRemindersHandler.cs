using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;

namespace Reminders.Application.Entities.Reminders.Queries.ListReminders;

internal class ListRemindersHandler(IRemindersService remindersService)
    : IQueryHandler<ListRemindersQuery, ListRemindersResult>
{
    public async Task<ListRemindersResult> Handle(ListRemindersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var reminders = await remindersService.ListRemindersPaginated(pageIndex, pageSize, cancellationToken);

        return new ListRemindersResult(
            new PaginatedResult<ReminderDto>(
                pageIndex,
                pageSize,
                reminders.Count,
                reminders));
    }
}
