using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

[JsonConverter(typeof(NoteIdJsonConverter))]
public record NoteId : StronglyTypedId<NoteId>
{
    private NoteId(Guid value) : base(value) { }

    public static NoteId Of(Guid value) => new(value);

    public class NoteIdJsonConverter : StronglyTypedIdJsonConverter<NoteId>;
}
