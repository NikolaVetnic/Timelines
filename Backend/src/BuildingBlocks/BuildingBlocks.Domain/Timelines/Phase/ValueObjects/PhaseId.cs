using BuildingBlocks.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BuildingBlocks.Domain.Timelines.Phase.ValueObjects;

[JsonConverter(typeof(PhaseIdJsonConverter))]
public class PhaseId : StronglyTypedId
{
    private PhaseId(Guid value) : base(value) { }

    public static PhaseId Of(Guid value) => new(value);

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj)
    {
        return obj is PhaseId other && Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}

public class PhaseIdValueConverter : ValueConverter<PhaseId, Guid>
{
    public PhaseIdValueConverter()
        : base(
            phaseId => phaseId.Value, // Convert from PhaseId to Guid
            guid => PhaseId.Of(guid)) // Convert from Guid to PhaseId
    {
    }
}

public class PhaseIdJsonConverter : JsonConverter<PhaseId>
{
    public override PhaseId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                {
                    var guidString = reader.GetString();
                    if (!Guid.TryParse(guidString, out var guid))
                        throw new JsonException($"Invalid GUID format for PhaseId: {guidString}");

                    return PhaseId.Of(guid);
                }
            case JsonTokenType.StartObject:
                {
                    using var jsonDoc = JsonDocument.ParseValue(ref reader);

                    if (!jsonDoc.RootElement.TryGetProperty("id", out JsonElement idElement))
                        throw new JsonException("Expected property 'id' not found.");

                    var guidString = idElement.GetString();

                    if (!Guid.TryParse(guidString, out var guid))
                        throw new JsonException($"Invalid GUID format for PhaseId: {guidString}");

                    return PhaseId.Of(guid);
                }
            default:
                throw new JsonException(
                    $"Unexpected token parsing PhaseId. Expected String or StartObject, got {reader.TokenType}.");
        }
    }

    public override void Write(Utf8JsonWriter writer, PhaseId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
