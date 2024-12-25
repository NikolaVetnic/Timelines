using BuildingBlocks.Domain.Exceptions;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

public record FileId
{
    private FileId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static FileId Of(Guid value)
    {
        if (value == Guid.Empty) throw new DomainException("FileId cannot be empty.");

        return new FileId(value);
    }
}