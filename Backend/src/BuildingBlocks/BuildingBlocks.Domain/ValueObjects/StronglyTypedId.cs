namespace BuildingBlocks.Domain.ValueObjects;

public abstract record StronglyTypedId<T>
    where T : StronglyTypedId<T>
{
    protected StronglyTypedId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException($"{typeof(T).Name} cannot be empty.", nameof(value));

        Value = value;
    }

    public Guid Value { get; }

    public override string ToString() => Value.ToString();
}
