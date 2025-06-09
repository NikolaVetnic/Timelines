using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using System.Linq.Expressions;

namespace Files.Application.Data.Abstractions;

public interface IFilesRepository
{
    Task AddFileAssetAsync(FileAsset fileAsset, CancellationToken cancellationToken);

    Task<List<FileAsset>> ListFileAssetsPaginatedAsync(Expression<Func<FileAsset, bool>> predicate, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<FileAsset>> ListFlaggedForDeletionFileAssetsPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<List<FileAsset>> ListFileAssetsByNodeIdPaginatedAsync(NodeId nodeId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<long> CountFileAssetsAsync(Expression<Func<FileAsset, bool>> predicate, CancellationToken cancellationToken);
    Task<FileAsset> GetFileAssetByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken);
    Task UpdateFileAssetAsync(FileAsset fileAsset, CancellationToken cancellationToken);

    Task DeleteFileAsset(FileAssetId fileAssetId, CancellationToken cancellationToken);
    Task DeleteFileAssets(IEnumerable<FileAssetId> fileAssetIds, CancellationToken cancellationToken);
    Task DeleteFileAssetsByNodeIds(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);

    Task ReviveFileAsset(FileAssetId fileAssetId, CancellationToken  cancelToken);

    Task<IEnumerable<FileAsset>> GetFileAssetsBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
}
