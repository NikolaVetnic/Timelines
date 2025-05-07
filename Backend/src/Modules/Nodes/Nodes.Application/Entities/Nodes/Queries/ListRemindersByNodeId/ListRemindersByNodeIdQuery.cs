using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;

namespace Nodes.Application.Entities.Nodes.Queries.ListRemindersByNodeId;

public record ListRemindersByNodeIdQuery(NodeId Id, PaginationRequest PaginationRequest) : IQuery<ListRemindersByNodeIdResult>
{
    public ListRemindersByNodeIdQuery(string id, PaginationRequest paginationRequest)
        : this(NodeId.Of(Guid.Parse(id)), paginationRequest) { }
}

public record ListRemindersByNodeIdResult(PaginatedResult<ReminderBaseDto> Reminders);
