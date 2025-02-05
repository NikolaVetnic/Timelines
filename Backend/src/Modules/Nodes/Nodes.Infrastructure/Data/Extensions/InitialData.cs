using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Nodes.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Node> Nodes =>
        new List<Node>
        {
            Node.Create(
                NodeId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97")),
                "Start Court Proceedings",
                "Beginning of the court proceedings in John Doe vs New York City.",
                "Preparation",
                DateTime.UtcNow,
                0,
                ["court", "complaint"],
                ["investigation", "interview"],
                TimelineId.Of(Guid.Parse("d87c1c54-b287-4a82-b45b-65db1d71e2fd"))
            ),

            Node.Create(
                NodeId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970")),
                "Witness Cross-Examination",
                "Cross-examination of the witness is due on 16 January 2025.",
                "Investigation",
                DateTime.UtcNow,
                1,
                ["court", "hearing"],
                ["witness", "vital"],
                TimelineId.Of(Guid.Parse("f739b2c7-883e-46c6-917c-d29114e3e017"))
            )
        };
}
