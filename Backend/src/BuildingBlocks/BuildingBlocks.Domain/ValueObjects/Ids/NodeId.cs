using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

[JsonConverter(typeof(NodeIdJsonConverter))]
public record NodeId : StronglyTypedId<NodeId>
{
    private NodeId(Guid value) : base(value) { }

    public static NodeId Of(Guid value) => new(value);
}

public class NodeIdJsonConverter : StronglyTypedIdJsonConverter<NodeId>;
