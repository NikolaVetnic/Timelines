using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using Files.Application.Data.Abstractions;
using Files.Application.Entities.Files.Exceptions;

namespace Files.Infrastructure.Data.Repositories;

public class FilesRepository(IFilesDbContext dbContext) : IFilesRepository
{
    public async Task<List<FileAsset>> ListFileAssetsPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.FileAssets
            .AsNoTracking()
            .OrderBy(n => n.CreatedBy)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<FileAsset>> ListFileAssetsByNodeIdPaginatedAsync(NodeId nodeId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.FileAssets
            .AsNoTracking()
            .Where(f => f.NodeId == nodeId)
            .OrderBy(f => f.CreatedAt)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<FileAsset> GetFileAssetByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        return await dbContext.FileAssets
                   .AsNoTracking()
                   .SingleOrDefaultAsync(f => f.Id == fileAssetId, cancellationToken) ??
               throw new FileAssetNotFoundException(fileAssetId.ToString());
    }

    public async Task UpdateFileAssetAsync(FileAsset fileAsset, CancellationToken cancellationToken)
    {
        dbContext.FileAssets.Update(fileAsset);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<long> FileAssetCountAsync(CancellationToken cancellationToken)
    {
        return await dbContext.FileAssets.LongCountAsync(cancellationToken);
    }

    public async Task<long> FileAssetCountByNodeIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        return await dbContext.FileAssets.CountAsync(f => f.NodeId == nodeId, cancellationToken);
    }

    public async Task DeleteFileAsset(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var fileAssetToDelete = await dbContext.FileAssets
            .FirstAsync(f => f.Id == fileAssetId, cancellationToken);

        dbContext.FileAssets.Remove(fileAssetToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteFileAssets(IEnumerable<FileAssetId> fileAssetIds, CancellationToken cancellationToken)
    {
        var fileAssetsToDelete = await dbContext.FileAssets
            .Where(f => fileAssetIds.Contains(f.Id))
            .ToListAsync(cancellationToken);

        dbContext.FileAssets.RemoveRange(fileAssetsToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<FileAsset>> GetFileAssetsBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        return await dbContext.FileAssets
            .AsNoTracking()
            .Where(f => nodeIds.Contains(f.NodeId))
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
