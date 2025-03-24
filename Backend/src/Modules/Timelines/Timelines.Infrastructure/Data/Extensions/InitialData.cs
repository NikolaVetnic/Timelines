using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Timeline> Timelines =>
        new List<Timeline>
        {
            new()
            {
                Id = TimelineId.Of(Guid.Parse("d87c1c54-b287-4a82-b45b-65db1d71e2fd")),
                Title = "Timeline 1",
                NodeIds = [NodeId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97"))]
            },
            new()
            {
                Id = TimelineId.Of(Guid.Parse("f739b2c7-883e-46c6-917c-d29114e3e017")),
                Title = "Timeline 2",
                NodeIds = [NodeId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970"))]
            }
        };
}
