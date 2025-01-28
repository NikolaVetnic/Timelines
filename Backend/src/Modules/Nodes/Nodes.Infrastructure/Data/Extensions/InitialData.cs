using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

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
                ["investigation", "interview"]
            ),

            Node.Create(
                NodeId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970")),
                "Witness Cross-Examination",
                "Cross-examination of the witness is due on 16 January 2025.",
                "Investigation",
                DateTime.UtcNow,
                1,
                ["court", "hearing"],
                ["witness", "vital"]
            )
        };
}
