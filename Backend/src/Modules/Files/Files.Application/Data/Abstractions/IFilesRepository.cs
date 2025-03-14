using BuildingBlocks.Domain.Files.File.ValueObjects;

namespace Files.Application.Data.Abstractions;

public interface IFilesRepository
{
    Task<FileAsset> GetFileAssetByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken);
    Task UpdateFileAssetAsync(FileAsset fileAsset, CancellationToken cancellationToken);
}
