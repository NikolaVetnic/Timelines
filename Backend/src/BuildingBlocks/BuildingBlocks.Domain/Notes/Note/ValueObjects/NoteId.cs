using BuildingBlocks.Domain.Abstractions;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BuildingBlocks.Domain.Notes.Note.ValueObjects;

[JsonConverter(typeof(NoteIdJsonConverter))]
public class NoteId : StronglyTypedId
{
    private NoteId(Guid value) : base(value) { }

    public static NoteId Of(Guid value) => new(value);

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj)
    {
        return obj is NoteId other && Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}

public class NoteIdJsonConverter : JsonConverter<NoteId>
{
    public override NoteId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                {
                    var guidString = reader.GetString();
                    if (!Guid.TryParse(guidString, out var guid))
                        throw new JsonException($"Invalid GUID format for NoteId: {guidString}");

                    return NoteId.Of(guid);
                }
            case JsonTokenType.StartObject:
                {
                    using var jsonDoc = JsonDocument.ParseValue(ref reader);

                    if (!jsonDoc.RootElement.TryGetProperty("id", out JsonElement idElement))
                        throw new JsonException("Expected property 'id' not found.");

                    var guidString = idElement.GetString();

                    if (!Guid.TryParse(guidString, out var guid))
                        throw new JsonException($"Invalid GUID format for NoteId: {guidString}");

                    return NoteId.Of(guid);
                }
            default:
                throw new JsonException(
                    $"Unexpected token parsing NoteId. Expected String or StartObject, got {reader.TokenType}.");
        }
    }

    public override void Write(Utf8JsonWriter writer, NoteId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}