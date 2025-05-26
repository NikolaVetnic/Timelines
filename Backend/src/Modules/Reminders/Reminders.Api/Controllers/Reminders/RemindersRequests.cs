using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using System;

namespace Reminders.Api.Controllers.Reminders;

public record CreateReminderRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime NotifyAt { get; set; }
    public int Priority { get; set; } // todo: This should be an enum common for all Priority properties
    public string ColorHex { get; set; }
    public string OwnerId { get; set; }
    public NodeId NodeId { get; set; }
}

public record UpdateReminderRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime NotifyAt { get; set; }
    public int Priority { get; set; }
    public string ColorHex { get; set; }
    public NodeId NodeId { get; set; }
}
