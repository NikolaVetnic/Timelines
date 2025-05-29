using System.Text.Json;
using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;

[JsonConverter(typeof(PhysicalPersonIdJsonConverter))]
public class PhysicalPersonId : StronglyTypedId
{
    private PhysicalPersonId(Guid value) : base(value) { }

    public static PhysicalPersonId Of(Guid value) => new(value);

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj)
    {
        return obj is PhysicalPersonId other && Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}

public class PhysicalPersonIdValueConverter : ValueConverter<PhysicalPersonId, Guid>
{
    public PhysicalPersonIdValueConverter()
        : base(
            physicalPersonId => physicalPersonId.Value, // Convert from PhysicalPersonId to Guid
            guid => PhysicalPersonId.Of(guid)) // Convert from Guid to PhysicalPersonId
    {
    }
}

public class PhysicalPersonIdJsonConverter : JsonConverter<PhysicalPersonId>
{
    public override PhysicalPersonId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                {
                    var guidString = reader.GetString();
                    if (!Guid.TryParse(guidString, out var guid))
                        throw new JsonException($"Invalid GUID format for PhysicalPersonId: {guidString}");

                    return PhysicalPersonId.Of(guid);
                }
            case JsonTokenType.StartObject:
                {
                    using var jsonDoc = JsonDocument.ParseValue(ref reader);

                    if (!jsonDoc.RootElement.TryGetProperty("id", out JsonElement idElement))
                        throw new JsonException("Expected property 'id' not found.");

                    var guidString = idElement.GetString();

                    if (!Guid.TryParse(guidString, out var guid))
                        throw new JsonException($"Invalid GUID format for PhysicalPersonId: {guidString}");

                    return PhysicalPersonId.Of(guid);
                }
            default:
                throw new JsonException(
                    $"Unexpected token parsing PhysicalPersonId. Expected String or StartObject, got {reader.TokenType}.");
        }
    }

    public override void Write(Utf8JsonWriter writer, PhysicalPersonId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
