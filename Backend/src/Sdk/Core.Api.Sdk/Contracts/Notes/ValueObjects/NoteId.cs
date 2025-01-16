namespace Core.Api.Sdk.Contracts.Notes.ValueObjects;

public class NoteId
{
    public NoteId() { }

    public NoteId(Guid value) => Value = value;

    public Guid Value { get; init; }
}
