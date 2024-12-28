using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

[JsonConverter(typeof(NodeIdJsonConverter))]
public record NodeId
{
    private NodeId(Guid value) => Value = value;

    public Guid Value { get; }

    public static NodeId Of(Guid value)
    {
        if (value == Guid.Empty) 
            throw new EmptyIdException("NodeId cannot be empty.");

        return new NodeId(value);
    }

    public override string ToString() => Value.ToString();
}

public class NodeIdJsonConverter : JsonConverter<NodeId>
{
    public override NodeId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        
        if (Guid.TryParse(value, out var guid))
            return NodeId.Of(guid);
        
        throw new JsonException($"Invalid GUID format for NodeId: {value}");
    }

    public override void Write(Utf8JsonWriter writer, NodeId value, JsonSerializerOptions options) => 
        writer.WriteStringValue(value.Value.ToString());
}
