﻿using System.Text.Json;
using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

[JsonConverter(typeof(TimelineIdJsonConverter))]
public class TimelineId : StronglyTypedId
{
    private TimelineId(Guid value) : base(value) { }

    public static TimelineId Of(Guid value) => new(value);

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj)
    {
        return obj is TimelineId other && Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}

public class TimelineIdValueConverter : ValueConverter<TimelineId, Guid>
{
    public TimelineIdValueConverter()
        : base(
            timelineId => timelineId.Value, // Convert from TimelineId to Guid
            guid => TimelineId.Of(guid)) // Convert from Guid to TimelineId
    {
    }
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
