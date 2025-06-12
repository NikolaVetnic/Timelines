using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;

namespace Reminders.Application.Entities.Reminders.Queries.ListFlaggedForDeletionReminders;

public record ListFlaggedForDeletionRemindersQuery(PaginationRequest PaginationRequest) : IQuery<ListFlaggedForDeletionRemindersResult>;

public record ListFlaggedForDeletionRemindersResult(PaginatedResult<ReminderDto> Reminders);
