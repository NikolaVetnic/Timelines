using System.Text.Json;
using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

[JsonConverter(typeof(TimelineIdJsonConverter))]
public class TimelineId : StronglyTypedId
{
    private TimelineId(Guid value) : base(value) { }

    public static TimelineId Of(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}

public class TimelineIdJsonConverter : JsonConverter<TimelineId>
{
    public override TimelineId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
            {
                var guidString = reader.GetString();
                if (!Guid.TryParse(guidString, out var guid))
                    throw new JsonException($"Invalid GUID format for TimelineId: {guidString}");

                return TimelineId.Of(guid);
            }
            case JsonTokenType.StartObject:
            {
                using var jsonDoc = JsonDocument.ParseValue(ref reader);

                if (!jsonDoc.RootElement.TryGetProperty("id", out JsonElement idElement))
                    throw new JsonException("Expected property 'id' not found.");

                var guidString = idElement.GetString();

                if (!Guid.TryParse(guidString, out var guid))
                    throw new JsonException($"Invalid GUID format for TimelineId: {guidString}");

                return TimelineId.Of(guid);
            }
            default:
                throw new JsonException(
                    $"Unexpected token parsing TimelineId. Expected String or StartObject, got {reader.TokenType}.");
        }
    }

    public override void Write(Utf8JsonWriter writer, TimelineId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
