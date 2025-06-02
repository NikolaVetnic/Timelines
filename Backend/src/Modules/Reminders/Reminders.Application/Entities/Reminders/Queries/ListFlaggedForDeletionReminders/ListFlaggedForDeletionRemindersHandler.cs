using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;

namespace Reminders.Application.Entities.Reminders.Queries.ListFlaggedForDeletionReminders;

internal class ListFlaggedForDeletionRemindersHandler(IRemindersService remindersService) : IQueryHandler<ListFlaggedForDeletionRemindersQuery, ListFlaggedForDeletionRemindersResult>
{
    public async Task<ListFlaggedForDeletionRemindersResult> Handle(ListFlaggedForDeletionRemindersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var reminders = await remindersService.ListFlaggedForDeletionRemindersPaginated(pageIndex, pageSize, cancellationToken);

        var totalCount = reminders.Count;

        return new ListFlaggedForDeletionRemindersResult(
            new PaginatedResult<ReminderDto>(
                pageIndex,
                pageSize,
                totalCount,
                reminders));
    }
}
