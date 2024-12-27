using BuildingBlocks.Domain.Exceptions;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

public record ReminderId
{
    private ReminderId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ReminderId Of(Guid value)
    {
        if (value == Guid.Empty) throw new EmptyIdException("ReminderId cannot be empty.");
    
        return new ReminderId(value);
    }
}
