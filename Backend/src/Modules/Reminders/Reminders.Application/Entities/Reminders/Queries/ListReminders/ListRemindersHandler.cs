using BuildingBlocks.Application.Pagination;
using Reminders.Application.Entities.Reminders.Dtos;
using Reminders.Application.Entities.Reminders.Extensions;

namespace Reminders.Application.Entities.Reminders.Queries.ListReminders;

public class ListRemindersHandler(IRemindersDbContext dbContext) : IQueryHandler<ListRemindersQuery, ListRemindersResult>
{
    public async Task<ListRemindersResult> Handle(ListRemindersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Reminders.LongCountAsync(cancellationToken);

        var nodes = await dbContext.Reminders
            .AsNoTracking()
            .OrderBy(r => r.NotifyAt)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        return new ListRemindersResult(
            new PaginatedResult<ReminderDto>(
                pageIndex,
                pageSize,
                totalCount,
                nodes.ToReminderDtoList()));
    }
}
