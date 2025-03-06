using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using Files.Application.Data.Abstractions;
using Files.Application.Entities.Files.Extensions;
using Mapster;

namespace Files.Application.Data;

public class FileAssetsService(IFilesRepository filesRepository, IServiceProvider serviceProvider) : IFileAssetsService
{
    public async Task<FileAssetDto> GetFileAssetByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var file = await filesRepository.GetFileAssetByIdAsync(fileAssetId, cancellationToken);
        var fileDto = file.ToFileAssetDto();

        return fileDto;
    }

    public async Task<FileAssetBaseDto> GetFileAssetBaseById(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var file = await filesRepository.GetFileAssetByIdAsync(fileAssetId, cancellationToken);
        var fileBaseDto = file.Adapt<FileAssetBaseDto>();

        return fileBaseDto;
    }
}
