using System.Text.Json.Serialization;
using System.Text.Json;

namespace BuildingBlocks.Domain.ValueObjects;

public abstract record StronglyTypedId
{
    protected StronglyTypedId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException($"{GetType().Name} cannot be empty.", nameof(value));

        Value = value;
    }

    public Guid Value { get; }

    public override string ToString() => Value.ToString();
}

public class StronglyTypedIdJsonConverter : JsonConverter<StronglyTypedId>
{
    public override StronglyTypedId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        if (Guid.TryParse(value, out var guid))
        {
            var constructor = typeToConvert.GetConstructor(new[] { typeof(Guid) });

            if (constructor != null)
                return (StronglyTypedId)constructor.Invoke(new object[] { guid });
        }

        throw new JsonException($"Invalid GUID format for {typeToConvert.Name}: {value}");
    }

    public override void Write(Utf8JsonWriter writer, StronglyTypedId value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Value.ToString());
}

