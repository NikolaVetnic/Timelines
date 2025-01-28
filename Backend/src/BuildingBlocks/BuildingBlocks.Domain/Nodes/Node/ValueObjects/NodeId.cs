using System.Text.Json;
using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

[JsonConverter(typeof(NodeIdJsonConverter))]
public class NodeId : StronglyTypedId
{
    private NodeId(Guid value) : base(value) { }

    public static NodeId Of(Guid value) => new(value);

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj)
    {
        return obj is NodeId other && Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}

public class NodeIdValueConverter : ValueConverter<NodeId, Guid>
{
    public NodeIdValueConverter()
        : base(
            nodeId => nodeId.Value, // Convert from NodeId to Guid
            guid => NodeId.Of(guid)) // Convert from Guid to NodeId
    {
    }
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
