namespace Timelines.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Timeline> Timelines =>
        new List<Timeline>
        {
            Timeline.Create(
                TimelineId.Of(Guid.Parse("d87c1c54-b287-4a82-b45b-65db1d71e2fd")),
                "Timeline 1"),

            Timeline.Create(
                TimelineId.Of(Guid.Parse("f739b2c7-883e-46c6-917c-d29114e3e017")),
                "Timeline 2")
        };
}