using BuildingBlocks.Domain.Exceptions;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

public record NoteId
{
    private NoteId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static NoteId Of(Guid value)
    {
        if (value == Guid.Empty) throw new DomainException("NoteId cannot be empty.");

        return new NoteId(value);
    }
}
