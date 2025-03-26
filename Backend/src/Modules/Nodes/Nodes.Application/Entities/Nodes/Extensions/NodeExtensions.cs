using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

namespace Nodes.Application.Entities.Nodes.Extensions;

public static class NodeExtensions
{
    public static NodeDto ToNodeDto(this Node node, IEnumerable<ReminderBaseDto> reminders, TimelineBaseDto timeline)
    {
        return new NodeDto(
            node.Id.ToString(),
            node.Title,
            node.Description,
            node.Timestamp,
            node.Importance,
            node.Phase,
            node.Categories.ToList(),
            node.Tags.ToList())
        {
            Reminders = reminders.ToList(),
            Timeline = timeline
        };
    }
}
