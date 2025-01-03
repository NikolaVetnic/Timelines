namespace BuildingBlocks.Domain.ValueObjects.Ids;

public class ReminderId : StronglyTypedId
{
    private ReminderId(Guid value) : base(value) { }

    public static ReminderId Of(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}
