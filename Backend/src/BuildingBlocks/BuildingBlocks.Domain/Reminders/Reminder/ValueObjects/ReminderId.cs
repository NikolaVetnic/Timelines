using System.Text.Json;
using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

[JsonConverter(typeof(ReminderIdJsonConverter))]
public class ReminderId : StronglyTypedId
{
    private ReminderId(Guid value) : base(value) { }

    public static ReminderId Of(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}

public class ReminderIdJsonConverter : JsonConverter<ReminderId>
{
    public override ReminderId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                {
                    var guidString = reader.GetString();
                    if (!Guid.TryParse(guidString, out var guid))
                        throw new JsonException($"Invalid GUID format for ReminderId: {guidString}");

                    return ReminderId.Of(guid);
                }
            case JsonTokenType.StartObject:
                {
                    using var jsonDoc = JsonDocument.ParseValue(ref reader);

                    if (!jsonDoc.RootElement.TryGetProperty("id", out JsonElement idElement))
                        throw new JsonException("Expected property 'id' not found.");

                    var guidString = idElement.GetString();

                    if (!Guid.TryParse(guidString, out var guid))
                        throw new JsonException($"Invalid GUID format for ReminderId: {guidString}");

                    return ReminderId.Of(guid);
                }
            default:
                throw new JsonException(
                    $"Unexpected token parsing ReminderId. Expected String or StartObject, got {reader.TokenType}.");
        }
    }

    public override void Write(Utf8JsonWriter writer, ReminderId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
