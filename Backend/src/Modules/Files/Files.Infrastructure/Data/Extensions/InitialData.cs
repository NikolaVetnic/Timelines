namespace Files.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<FileAsset> FileAssets =>
        new List<FileAsset>
        {
            FileAsset.Create(
                FileAssetId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97")),
                "Document 1",
                1.5f,
                "pdf",
                "Owner1",
                "Description for Document 1",
                ["User1", "User2"]
            ),

            FileAsset.Create(
                FileAssetId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970")),
                "Document 2",
                2.3f,
                "docx",
                "Owner2",
                "Description for Document 2",
                ["User3", "User4"]
            )
        };
}
