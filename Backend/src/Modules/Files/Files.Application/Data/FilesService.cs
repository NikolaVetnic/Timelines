using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using Files.Application.Data.Abstractions;
using Files.Application.Entities.Files.Extensions;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Files.Application.Data;

public class FilesService(IServiceProvider serviceProvider, IFilesRepository filesRepository) : IFilesService
{
    private INodesService NodesService => serviceProvider.GetRequiredService<INodesService>();

    #region List

    public async Task<List<FileAssetDto>> ListFileAssetsPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var nodes = await NodesService.ListNodesPaginated(pageIndex, pageSize, cancellationToken);

        var fileAssets = await filesRepository.ListFileAssetsPaginatedAsync(f => !f.IsDeleted, pageIndex, pageSize, cancellationToken);

        var fileAssetsDtos = fileAssets.Select(f =>
            f.ToFileAssetDto(
                nodes.First(n => n.Id == f.NodeId.ToString())
            )).ToList();

        return fileAssetsDtos;
    }

    public async Task<List<FileAssetDto>> ListFlaggedForDeletionFileAssetsPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var nodes = await NodesService.ListNodesPaginated(pageIndex, pageSize, cancellationToken);

        var fileAssets = await filesRepository.ListFileAssetsPaginatedAsync(f => f.IsDeleted, pageIndex, pageSize, cancellationToken);

        var fileAssetsDtos = fileAssets.Select(f =>
            f.ToFileAssetDto(
                nodes.First(n => n.Id == f.NodeId.ToString())
            )).ToList();

        return fileAssetsDtos;
    }

    public async Task<List<FileAssetBaseDto>> ListFileAssetsByNodeIdPaginated(NodeId nodeId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var fileAssets = await filesRepository.ListFileAssetsPaginatedAsync(f => f.NodeId == nodeId, pageIndex, pageSize, cancellationToken);

        var fileAssetsDtos = fileAssets
            .Select(f => f.ToFileAssetBaseDto())
            .ToList();

        return fileAssetsDtos;
    }

    public async Task<long> CountFileAssetsAsync(CancellationToken cancellationToken)
    {
        return await filesRepository.CountFileAssetsAsync(n => true, cancellationToken);
    }

    public async Task<long> CountFileAssetsByNodeIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        return await filesRepository.CountFileAssetsAsync(f => f.NodeId == nodeId, cancellationToken);
    }

    #endregion

    #region Get

    public async Task<FileAssetDto> GetFileAssetByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var file = await filesRepository.GetFileAssetByIdAsync(fileAssetId, cancellationToken);
        var fileDto = file.Adapt<FileAssetDto>();

        var node = await NodesService.GetNodeBaseByIdAsync(file.NodeId, cancellationToken);
        fileDto.Node = node;

        return fileDto;
    }

    public async Task<FileAssetBaseDto> GetFileAssetBaseByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var file = await filesRepository.GetFileAssetByIdAsync(fileAssetId, cancellationToken);
        var fileBaseDto = file.Adapt<FileAssetBaseDto>();

        return fileBaseDto;
    }

    public async Task<List<FileAssetBaseDto>> GetFileAssetsBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        var fileAssets = await filesRepository.GetFileAssetsBaseBelongingToNodeIdsAsync(nodeIds, cancellationToken);
        var fileAssetBaseDtos = fileAssets.Adapt<List<FileAssetBaseDto>>();

        return fileAssetBaseDtos;
    }

    public async Task<long> CountAllFileAssetsAsync(CancellationToken cancellationToken)
    {
        return await filesRepository.CountFileAssetsAsync(f => true, cancellationToken);
    }

    public async Task<long> CountAllFileAssetsByNodeIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        return await filesRepository.CountFileAssetsAsync(f => f.NodeId == nodeId, cancellationToken);
    }

    #endregion

    public async Task DeleteFileAsset(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var fileAsset = await filesRepository.GetFileAssetByIdAsync(fileAssetId, cancellationToken);

        await NodesService.RemoveFileAsset(fileAsset.NodeId, fileAssetId, cancellationToken);

        await filesRepository.DeleteFileAsset(fileAssetId, cancellationToken);
    }

    public async Task DeleteFiles(NodeId nodeId, IEnumerable<FileAssetId> fileAssetIds, CancellationToken cancellationToken)
    {
        var input = fileAssetIds.ToList();

        await filesRepository.DeleteFileAssets(input, cancellationToken);
    }

    public async Task DeleteFileAssetsByNodeIds(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        await filesRepository.DeleteFileAssetsByNodeIds(nodeIds, cancellationToken);
    }

    public async Task ReviveFileAsset(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        await filesRepository.ReviveFileAsset(fileAssetId, cancellationToken);
    }
}
