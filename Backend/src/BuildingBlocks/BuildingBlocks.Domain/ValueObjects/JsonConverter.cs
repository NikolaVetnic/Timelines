﻿using System.Text.Json.Serialization;
using System.Text.Json;

namespace BuildingBlocks.Domain.ValueObjects;

public class StronglyTypedIdJsonConverter<T> : JsonConverter<T>
    where T : StronglyTypedId<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        if (Guid.TryParse(value, out var guid))
        {
            var constructor = typeof(T).GetConstructor(new[] { typeof(Guid) });

            if (constructor != null)
                return (T)constructor.Invoke(new object[] { guid });
        }

        throw new JsonException($"Invalid GUID format for {typeof(T).Name}: {value}");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Value.ToString());
}