using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

[JsonConverter(typeof(TimelineIdJsonConverter))]
public record TimelineId : StronglyTypedId
{
    private TimelineId(Guid value) : base(value) { }

    public static TimelineId Of(Guid value) => new(value);

    public class TimelineIdJsonConverter : StronglyTypedIdJsonConverter<TimelineId>;
}
