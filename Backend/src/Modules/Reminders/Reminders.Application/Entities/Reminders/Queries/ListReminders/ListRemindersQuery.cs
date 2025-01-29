using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Reminders.Dtos;

// ReSharper disable ClassNeverInstantiated.Global

namespace Reminders.Application.Entities.Reminders.Queries.ListReminders;

public record ListRemindersQuery(PaginationRequest PaginationRequest) : IQuery<ListRemindersResult>;

public record ListRemindersResult(PaginatedResult<ReminderDto> Reminders);
