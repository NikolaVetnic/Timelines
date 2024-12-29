using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

[JsonConverter(typeof(ReminderIdJsonConverter))]
public record ReminderId : StronglyTypedId
{
    private ReminderId(Guid value) : base(value) { }

    public static ReminderId Of(Guid value) => new(value);

    public class ReminderIdJsonConverter : StronglyTypedIdJsonConverter<ReminderId>;
}
