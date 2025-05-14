using BuildingBlocks.Domain.Enums;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace Files.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<FileAsset> FileAssets =>
        new List<FileAsset>
        {
            new()
            {
                Id = FileAssetId.Of(Guid.Parse("d79293f2-4910-44e2-bfbb-690a6f24f703")),
                Name = "Document 1",
                Description = "Description for Document 1",
                Size = 1.5f,
                Type = EFileType.Pdf,
                OwnerId = "11111111-1111-1111-1111-111111111111",
                Content = [],
                IsPublic = true,
                SharedWith = ["User1", "User2"],
                NodeId = NodeId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97"))
            },

            new()
            {
                Id = FileAssetId.Of(Guid.Parse("16d56e5f-dcea-4b1f-82e3-4c0fdb142773")),
                Name = "Document 2",
                Description = "Description for Document 2",
                Size = 2.3f,
                Type = EFileType.Docx,
                OwnerId = "22222222-2222-2222-2222-222222222222",
                Content = [],
                IsPublic = true,
                SharedWith = ["User3", "User4"],
                NodeId = NodeId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970"))
            }
        };
}
