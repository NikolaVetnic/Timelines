using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

public class NoteId : StronglyTypedId
{
    private NoteId(Guid value) : base(value) { }

    public static NoteId Of(Guid value) => new(value);
    
    public override string ToString() => Value.ToString();
}
