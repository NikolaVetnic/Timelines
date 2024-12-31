using Files.Application.Entities.Files.Dtos;

namespace Files.Application.Entities.Files.Extensions;

public static class FileAssetExtensions
{
    public static FileAssetDto ToFileAssetDto(this FileAsset fileAsset)
    {
        return new FileAssetDto(
            fileAsset.Id.ToString(),
            fileAsset.Name,
            fileAsset.Size,
            fileAsset.Type,
            fileAsset.Owner,
            fileAsset.Description,
            fileAsset.SharedWith.ToList());
    }

    public static IEnumerable<FileAssetDto> ToFileAssetDtoList(this IEnumerable<FileAsset> fileAssets)
    {
        return fileAssets.Select(ToFileAssetDto);
    }
}
