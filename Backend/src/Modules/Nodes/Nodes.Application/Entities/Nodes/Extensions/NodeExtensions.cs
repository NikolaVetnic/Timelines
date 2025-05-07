using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

namespace Nodes.Application.Entities.Nodes.Extensions;

public static class NodeExtensions
{
    public static NodeBaseDto ToNodeBaseDto(this Node node)
    {
        return new NodeBaseDto(
            node.Id.ToString(),
            node.Title,
            node.Description,
            node.Timestamp,
            node.Importance,
            node.Phase,
            node.Categories.ToList(),
            node.Tags.ToList());
    }

    public static NodeDto ToNodeDto(this Node node, TimelineBaseDto timeline, IEnumerable<FileAssetBaseDto> fileAssets, IEnumerable<NoteBaseDto> notes, IEnumerable<ReminderBaseDto> reminders)
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
            Timeline = timeline,
            FileAssets = fileAssets.ToList(),
            Notes = notes.ToList(),
            Reminders = reminders.ToList()
        };
    }
}
