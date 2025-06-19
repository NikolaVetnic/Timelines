using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface IFilesService
{
    Task<List<FileAssetDto>> ListFileAssetsPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<FileAssetDto>> ListFlaggedForDeletionFileAssetsPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<FileAssetBaseDto>> ListFileAssetsByNodeIdPaginated(NodeId nodeId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<FileAssetDto> GetFileAssetByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken);
    Task<FileAssetBaseDto> GetFileAssetBaseByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken);
    Task<long> CountAllFileAssetsAsync(CancellationToken cancellationToken);
    Task<long> CountAllFileAssetsByNodeIdAsync(NodeId nodeId, CancellationToken cancellationToken);

    Task DeleteFileAsset(FileAssetId fileAssetId, CancellationToken cancellationToken);
    Task DeleteFiles(NodeId nodeId, IEnumerable<FileAssetId> fileAssetIds, CancellationToken cancellationToken);
    Task DeleteFileAssetsByNodeIds(IEnumerable<NodeId> nodeId, CancellationToken cancellationToken);

    Task ReviveFileAsset(FileAssetId fileAssetId, CancellationToken cancellationToken);

    Task<List<FileAssetBaseDto>> GetFileAssetsBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
}
