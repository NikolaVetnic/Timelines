using BuildingBlocks.Domain.Exceptions;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

public record TimelineId
{
    private TimelineId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static TimelineId Of(Guid value)
    {
        if (value == Guid.Empty) throw new EmptyIdException("TimelineId cannot be empty.");

        return new TimelineId(value);
    }
}
