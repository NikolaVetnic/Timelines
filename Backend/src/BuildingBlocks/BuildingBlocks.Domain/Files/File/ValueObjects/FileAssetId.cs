using System.Text.Json;
using BuildingBlocks.Domain.Abstractions;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.Files.File.ValueObjects;

[JsonConverter(typeof(FileAssetIdJsonConverter))]
public class FileAssetId : StronglyTypedId
{
    private FileAssetId(Guid value) : base(value) { }

    public static FileAssetId Of(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}

public class FileAssetIdJsonConverter : JsonConverter<FileAssetId>
{
    public override FileAssetId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
            {
                var guidString = reader.GetString();
                if (!Guid.TryParse(guidString, out var guid))
                    throw new JsonException($"Invalid GUID format for FileAssetId: {guidString}");

                return FileAssetId.Of(guid);
            }
            case JsonTokenType.StartObject:
            {
                using var jsonDoc = JsonDocument.ParseValue(ref reader);

                if (!jsonDoc.RootElement.TryGetProperty("id", out JsonElement idElement))
                    throw new JsonException("Expected property 'id' not found.");

                var guidString = idElement.GetString();

                if (!Guid.TryParse(guidString, out var guid))
                    throw new JsonException($"Invalid GUID format for FileAssetId: {guidString}");

                return FileAssetId.Of(guid);
            }
            default:
                throw new JsonException(
                    $"Unexpected token parsing FileAssetId. Expected String or StartObject, got {reader.TokenType}.");
        }
    }

    public override void Write(Utf8JsonWriter writer, FileAssetId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
