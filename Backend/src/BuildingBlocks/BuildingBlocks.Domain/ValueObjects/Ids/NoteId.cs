using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

[JsonConverter(typeof(NoteIdJsonConverter))]
public record NoteId : StronglyTypedId
{
    private NoteId(Guid value) : base(value) { }

    public static NoteId Of(Guid value) => new(value);

    private class NoteIdJsonConverter : StronglyTypedIdJsonConverter<NoteId>;
}
