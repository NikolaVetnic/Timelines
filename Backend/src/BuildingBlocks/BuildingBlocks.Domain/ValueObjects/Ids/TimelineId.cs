namespace BuildingBlocks.Domain.ValueObjects.Ids;

public class TimelineId : StronglyTypedId
{
    private TimelineId(Guid value) : base(value) { }

    public static TimelineId Of(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}
