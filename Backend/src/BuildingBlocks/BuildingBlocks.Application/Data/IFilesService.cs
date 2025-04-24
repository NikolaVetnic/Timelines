using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Files.File.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface IFilesService
{
    Task<FileAssetDto> GetFileAssetByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken);
    Task<FileAssetBaseDto> GetFileAssetBaseByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken);

    Task DeleteFileAsset(FileAssetId fileAssetId, CancellationToken cancellationToken);
}
