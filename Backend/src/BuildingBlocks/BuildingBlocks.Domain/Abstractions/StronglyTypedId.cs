namespace BuildingBlocks.Domain.Abstractions;

public abstract class StronglyTypedId
{
    protected StronglyTypedId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException($"{GetType().Name} cannot be empty.", nameof(value));

        Value = value;
    }

    public Guid Value { get; protected set; }

    public abstract override string ToString();
}