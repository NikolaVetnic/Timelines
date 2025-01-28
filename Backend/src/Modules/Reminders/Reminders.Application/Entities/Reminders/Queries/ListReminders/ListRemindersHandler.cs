using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Reminders.Dtos;
using Reminders.Application.Data.Abstractions;
using Reminders.Application.Entities.Reminders.Extensions;

namespace Reminders.Application.Entities.Reminders.Queries.ListReminders;

public class ListRemindersHandler(IRemindersDbContext dbContext, INodesService nodesService) : IQueryHandler<ListRemindersQuery, ListRemindersResult>
{
    public async Task<ListRemindersResult> Handle(ListRemindersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Reminders.LongCountAsync(cancellationToken);

        var reminders = await dbContext.Reminders
            .AsNoTracking()
            .OrderBy(r => r.DueDateTime)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        var reminderDtos = reminders.Select(r =>
        {
            var node = nodesService.GetNodeBaseByIdAsync(r.NodeId, cancellationToken).GetAwaiter().GetResult();
            return r.ToReminderDto(node);
        }).ToList();

        return new ListRemindersResult(
            new PaginatedResult<ReminderDto>(
                pageIndex,
                pageSize,
                totalCount,
                reminderDtos));
    }
}
