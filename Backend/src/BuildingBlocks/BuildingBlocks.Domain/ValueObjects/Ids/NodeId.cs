using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

[JsonConverter(typeof(NodeIdJsonConverter))]
public class NodeId : StronglyTypedId
{
    private NodeId(Guid value) : base(value)
    {
    }

    public static NodeId Of(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}

public class NodeIdJsonConverter : JsonConverter<NodeId>
{
    public override NodeId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
            {
                var guidString = reader.GetString();
                if (!Guid.TryParse(guidString, out var guid))
                    throw new JsonException($"Invalid GUID format for NodeId: {guidString}");
                
                return NodeId.Of(guid);
            }
            case JsonTokenType.StartObject:
            {
                using var jsonDoc = JsonDocument.ParseValue(ref reader);

                if (!jsonDoc.RootElement.TryGetProperty("id", out JsonElement idElement))
                    throw new JsonException("Expected property 'id' not found.");

                var guidString = idElement.GetString();

                if (!Guid.TryParse(guidString, out var guid))
                    throw new JsonException($"Invalid GUID format for NodeId: {guidString}");

                return NodeId.Of(guid);
            }
            default:
                throw new JsonException(
                    $"Unexpected token parsing NodeId. Expected String or StartObject, got {reader.TokenType}.");
        }
    }

    public override void Write(Utf8JsonWriter writer, NodeId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
