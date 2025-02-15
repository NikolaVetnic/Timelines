using BuildingBlocks.Domain.Files.File.ValueObjects;

namespace Files.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<FileAsset> FileAssets =>
        new List<FileAsset>
        {
            FileAsset.Create(
                FileAssetId.Of(Guid.Parse("d79293f2-4910-44e2-bfbb-690a6f24f703")),
                "Document 1",
                1.5f,
                "pdf",
                "Owner1",
                "Description for Document 1",
                ["User1", "User2"]
            ),

            FileAsset.Create(
                FileAssetId.Of(Guid.Parse("16d56e5f-dcea-4b1f-82e3-4c0fdb142773")),
                "Document 2",
                2.3f,
                "docx",
                "Owner2",
                "Description for Document 2",
                ["User3", "User4"]
            )
        };
}
