using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using Files.Application.Data.Abstractions;
using Files.Application.Entities.Files.Extensions;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Files.Application.Data;

public class FilesService(IServiceProvider serviceProvider, IFilesRepository filesRepository) : IFilesService
{
    public async Task<List<FileAssetDto>> ListFileAssetsPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var nodesService = serviceProvider.GetRequiredService<INodesService>();
        var nodes = await nodesService.ListNodesPaginated(pageIndex, pageSize, cancellationToken);

        var fileAssets = await filesRepository.ListFileAssetsPaginatedAsync(pageIndex, pageSize, cancellationToken);

        var fileAssetsDtos = fileAssets.Select(f =>
            f.ToFileAssetDto(
                nodes.First(n => n.Id == f.NodeId.ToString())
            )).ToList();

        return fileAssetsDtos;
    }

    public async Task<FileAssetDto> GetFileAssetByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var file = await filesRepository.GetFileAssetByIdAsync(fileAssetId, cancellationToken);
        var fileDto = file.Adapt<FileAssetDto>();

        var node = await serviceProvider.GetRequiredService<INodesService>().GetNodeBaseByIdAsync(file.NodeId, cancellationToken);
        fileDto.Node = node;

        return fileDto;
    }

    public async Task<FileAssetBaseDto> GetFileAssetBaseByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var file = await filesRepository.GetFileAssetByIdAsync(fileAssetId, cancellationToken);
        var fileBaseDto = file.Adapt<FileAssetBaseDto>();

        return fileBaseDto;
    }

    public async Task<long> CountFileAssetsAsync(CancellationToken cancellationToken)
    {
        return await filesRepository.FileAssetCountAsync(cancellationToken);
    }

    public async Task DeleteFileAsset(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var fileAsset = await filesRepository.GetFileAssetByIdAsync(fileAssetId, cancellationToken);

        var nodeService = serviceProvider.GetRequiredService<INodesService>();
        await nodeService.RemoveFileAsset(fileAsset.NodeId, fileAssetId, cancellationToken);

        await filesRepository.DeleteFileAsset(fileAssetId, cancellationToken);
    }
}
