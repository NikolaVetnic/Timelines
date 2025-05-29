using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;

namespace Nodes.Application.Entities.Nodes.Queries.ListRemindersByNodeId;

internal class ListRemindersByNodeIdHandler(IRemindersService remindersService)
    : IQueryHandler<ListRemindersByNodeIdQuery, ListRemindersByNodeIdResult>
{
    public async Task<ListRemindersByNodeIdResult> Handle(ListRemindersByNodeIdQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await remindersService.CountRemindersByNodeIdAsync(query.Id, cancellationToken);

        var fileAssets = await remindersService.ListRemindersByNodeIdPaginated(query.Id, pageIndex, pageSize, cancellationToken);

        return new ListRemindersByNodeIdResult(
            new PaginatedResult<ReminderBaseDto>(
                pageIndex,
                pageSize,
                totalCount,
                fileAssets));
    }
}
