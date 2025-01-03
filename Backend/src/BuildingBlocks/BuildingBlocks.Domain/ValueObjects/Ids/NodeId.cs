namespace BuildingBlocks.Domain.ValueObjects.Ids;

public class NodeId : StronglyTypedId
{
    private NodeId(Guid value) : base(value) { }

    public static NodeId Of(Guid value) => new(value);
    
    public override string ToString() => Value.ToString();
}
