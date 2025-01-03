using BuildingBlocks.Domain.ValueObjects.Ids;

namespace BuildingBlocks.Api.Converters;

public class FileIdConverter : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<FileAssetId, FileAssetId>().ConstructUsing(src => FileAssetId.Of(src.Value));
}
