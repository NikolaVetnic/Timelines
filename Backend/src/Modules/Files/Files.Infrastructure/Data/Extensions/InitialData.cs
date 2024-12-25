using BuildingBlocks.Domain.ValueObjects.Ids;
using Files.Domain.Models;

namespace Files.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<FileAsset> FileAssets =>
        new List<FileAsset>
        {
            FileAsset.Create(
                FileAssetId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97")),
                "Title 1"
            ),

            FileAsset.Create(
                FileAssetId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970")),
                "Title 2"
            )
        };
}
