using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Nodes.Node.Dtos;

namespace Files.Application.Entities.Files.Extensions;

public static class FileAssetExtensions
{
    public static FileAssetDto ToFileAssetDto(this FileAsset fileAsset, NodeBaseDto node)
    {
        return new FileAssetDto(
            fileAsset.Id.ToString(),
            fileAsset.Name,
            fileAsset.Description,
            fileAsset.Size,
            fileAsset.Type,
            fileAsset.OwnerId,
            fileAsset.Content,
            fileAsset.IsPublic,
            fileAsset.SharedWith.ToList(),
            node);
    }

    public static FileAssetBaseDto ToFileAssetBaseDto(this FileAsset fileAsset)
    {
        return new FileAssetBaseDto(
            fileAsset.Id.ToString(),
            fileAsset.Name,
            fileAsset.Description,
            fileAsset.Size,
            fileAsset.Type,
            fileAsset.OwnerId,
            fileAsset.Content,
            fileAsset.IsPublic,
            fileAsset.SharedWith.ToList()
            );
    }
}
