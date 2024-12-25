using BuildingBlocks.Domain.Exceptions;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

public record FileAssetId
{
    private FileAssetId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static FileAssetId Of(Guid value)
    {
        if (value == Guid.Empty) throw new DomainException("FileId cannot be empty.");

        return new FileAssetId(value);
    }
}