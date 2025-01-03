namespace BuildingBlocks.Domain.ValueObjects;

public abstract class StronglyTypedId
{
    protected StronglyTypedId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException($"{GetType().Name} cannot be empty.", nameof(value));

        Value = value;
    }

    public Guid Value { get; }
    
    public abstract override string ToString(); 
}
