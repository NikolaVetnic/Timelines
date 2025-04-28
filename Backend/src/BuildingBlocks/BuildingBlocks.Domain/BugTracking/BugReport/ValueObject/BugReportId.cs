using System.Text.Json;
using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.Domain.BugTracking.BugReport.ValueObject;

[JsonConverter(typeof(BugReportIdJsonConverter))]
public class BugReportId : StronglyTypedId
{
    private BugReportId(Guid value) : base(value) { }

    public static BugReportId Of(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}

public class BugReportIdJsonConverter : JsonConverter<BugReportId>
{
    public override BugReportId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
            {
                var guidString = reader.GetString();
                if (!Guid.TryParse(guidString, out var guid))
                    throw new JsonException($"Invalid GUID format for BugReportId: {guidString}");

                return BugReportId.Of(guid);
            }
            case JsonTokenType.StartObject:
            {
                using var jsonDoc = JsonDocument.ParseValue(ref reader);

                if (!jsonDoc.RootElement.TryGetProperty("id", out JsonElement idElement))
                    throw new JsonException("Expected property 'id' not found.");

                var guidString = idElement.GetString();

                if (!Guid.TryParse(guidString, out var guid))
                    throw new JsonException($"Invalid GUID format for BugReportId: {guidString}");

                return BugReportId.Of(guid);
            }
            default:
                throw new JsonException(
                    $"Unexpected token parsing BugReportId. Expected String or StartObject, got {reader.TokenType}.");
        }
    }

    public override void Write(Utf8JsonWriter writer, BugReportId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
