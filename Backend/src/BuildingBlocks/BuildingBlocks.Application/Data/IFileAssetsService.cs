using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Files.File.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface IFileAssetsService
{
    Task<FileAssetDto> GetFileAssetByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken);
    Task<FileAssetBaseDto> GetFileAssetBaseById(FileAssetId fileAssetId, CancellationToken cancellationToken);
}
