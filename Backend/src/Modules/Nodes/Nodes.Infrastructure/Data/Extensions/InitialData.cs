using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
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
                NoteIds = [NoteId.Of(Guid.Parse("74dad71c-4ddc-4d4d-a894-3307ddc3fe10"))]
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
                NoteIds = [NoteId.Of(Guid.Parse("dffbedcb-b793-4ac2-8767-1fb391033644"))]
            }
        };
}
