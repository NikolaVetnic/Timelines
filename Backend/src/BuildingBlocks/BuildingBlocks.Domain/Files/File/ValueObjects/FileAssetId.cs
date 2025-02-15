using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.Domain.Files.File.ValueObjects;

public class FileAssetId : StronglyTypedId
{
    private FileAssetId(Guid value) : base(value) { }

    public static FileAssetId Of(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}
