using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Nodes.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Node> Nodes =>
        new List<Node>
        {
            new()
            {
                Id = NodeId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97")),
                Title = "Start Court Proceedings",
                Description = "Beginning of the court proceedings in John Doe vs New York City.",
                Phase = "Preparation",
                Timestamp = DateTime.UtcNow,
                Importance = 0,
                Categories = ["court", "complaint"],
                Tags = ["investigation", "interview"],
                TimelineId = TimelineId.Of(Guid.Parse("d87c1c54-b287-4a82-b45b-65db1d71e2fd")),
                FileAssetIds = [FileAssetId.Of(Guid.Parse("d79293f2-4910-44e2-bfbb-690a6f24f703"))],
                ReminderIds = [ReminderId.Of(Guid.Parse("2e1c4902-7841-484c-b997-0a8cd3955e72"))]
            },

            new()
            {
                Id = NodeId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970")),
                Title = "Witness Cross-Examination",
                Description = "Cross-examination of the witness is due on 16 January 2025.",
                Phase = "Investigation",
                Timestamp = DateTime.UtcNow,
                Importance = 1,
                Categories = ["court", "hearing"],
                Tags = ["witness", "vital"],
                TimelineId = TimelineId.Of(Guid.Parse("f739b2c7-883e-46c6-917c-d29114e3e017")),
                FileAssetIds = [FileAssetId.Of(Guid.Parse("16d56e5f-dcea-4b1f-82e3-4c0fdb142773"))],
                ReminderIds = [ReminderId.Of(Guid.Parse("74f40a78-bda2-4177-bffd-86e6648c4318"))]
            }
        };
}
