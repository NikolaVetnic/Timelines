using BuildingBlocks.Domain.Abstractions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.Users.User.ValueObjects;

public class UserId(Guid value) : StronglyTypedId(value), IComparable<UserId>, IEquatable<UserId>
{
    public static UserId Of(Guid value) => new(value);

    public int CompareTo(UserId? other) => Value.CompareTo(other.Value);

    public bool Equals(UserId? other) => Value.Equals(other.Value);

    public override string ToString() => Value.ToString();
}

public class BugReportIdJsonConverter : JsonConverter<UserId>
{
    public override UserId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
            {
                var guidString = reader.GetString();
                if (!Guid.TryParse(guidString, out var guid))
                    throw new JsonException($"Invalid GUID format for UserId: {guidString}");

                return UserId.Of(guid);
            }
            case JsonTokenType.StartObject:
            {
                using var jsonDoc = JsonDocument.ParseValue(ref reader);

                if (!jsonDoc.RootElement.TryGetProperty("id", out var idElement))
                    throw new JsonException("Expected property 'id' not found.");

                var guidString = idElement.GetString();

                if (!Guid.TryParse(guidString, out var guid))
                    throw new JsonException($"Invalid GUID format for UserId: {guidString}");

                return UserId.Of(guid);
            }
            default:
                throw new JsonException(
                    $"Unexpected token parsing UserId. Expected String or StartObject, got {reader.TokenType}.");
        }
    }

    public override void Write(Utf8JsonWriter writer, UserId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
