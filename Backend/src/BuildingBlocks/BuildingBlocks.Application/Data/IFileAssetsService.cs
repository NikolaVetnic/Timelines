using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Files.File.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface IFileAssetsService
{
    Task<FileAssetDto> GetFileAssetByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken);
    Task<FileAssetBaseDto> GetFileAssetBaseByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken);
}
