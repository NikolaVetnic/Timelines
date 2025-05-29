using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace Files.Application.Data.Abstractions;

public interface IFilesRepository
{
    Task AddFileAssetAsync(FileAsset fileAsset, CancellationToken cancellationToken);
  
    Task<List<FileAsset>> ListFileAssetsPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<FileAsset>> ListFileAssetsByNodeIdPaginatedAsync(NodeId nodeId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<FileAsset> GetFileAssetByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken);
    Task UpdateFileAssetAsync(FileAsset fileAsset, CancellationToken cancellationToken);
    Task<long> AllFileAssetCountAsync(CancellationToken cancellationToken);
    Task<long> FileAssetCountByNodeIdAsync(NodeId nodeId, CancellationToken cancellationToken);

    Task DeleteFileAsset(FileAssetId fileAssetId, CancellationToken cancellationToken);
    Task DeleteFileAssets(IEnumerable<FileAssetId> fileAssetIds, CancellationToken cancellationToken);
    Task DeleteFileAssetsByNodeIds(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);

    Task<IEnumerable<FileAsset>> GetFileAssetsBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
}
