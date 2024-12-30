using BuildingBlocks.Application.Pagination;
using Reminders.Application.Entities.Reminders.Dtos;

namespace Reminders.Application.Entities.Reminders.Queries.ListReminders;

public record ListRemindersQuery(PaginationRequest PaginationRequest) : IQuery<ListRemindersResult>;

public record ListRemindersResult(PaginatedResult<ReminderDto> Reminders);
