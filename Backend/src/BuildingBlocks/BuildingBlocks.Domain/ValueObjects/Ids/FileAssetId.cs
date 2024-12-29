using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

[JsonConverter(typeof(FileAssetIdJsonConverter))]
public record FileAssetId : StronglyTypedId
{
    private FileAssetId(Guid value) : base(value) { }

    public static FileAssetId Of(Guid value) => new(value);

    public class FileAssetIdJsonConverter : StronglyTypedIdJsonConverter<FileAssetId>;
}
