using BuildingBlocks.Domain.Enums;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace Files.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<FileAsset> FileAssets =>
        new List<FileAsset>
        {
            FileAsset.Create(
                FileAssetId.Of(Guid.Parse("d79293f2-4910-44e2-bfbb-690a6f24f703")),
                "Document 1",
                "Description for Document 1",
                1.5f,
                EFileType.Pdf,
                "Owner1",
                [],
                true,
                ["User1", "User2"],
                NodeId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97"))
            ),

            FileAsset.Create(
                FileAssetId.Of(Guid.Parse("16d56e5f-dcea-4b1f-82e3-4c0fdb142773")),
                "Document 2",
                "Description for Document 2",
                2.3f,
                EFileType.Docx,
                "Owner2",
                [],
                true,
                ["User3", "User4"],
                NodeId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970"))
            )
        };
}