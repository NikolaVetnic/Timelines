﻿using System.Linq.Expressions;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using Files.Application.Data.Abstractions;
using Files.Application.Entities.Files.Exceptions;

namespace Files.Infrastructure.Data.Repositories;

public class FilesRepository(ICurrentUser currentUser, IFilesDbContext dbContext) : IFilesRepository
{
    public async Task AddFileAssetAsync(FileAsset fileAsset, CancellationToken cancellationToken)
    {
        dbContext.FileAssets.Add(fileAsset);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<FileAsset>> ListFileAssetsPaginatedAsync(Expression<Func<FileAsset, bool>> predicate, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.FileAssets
            .AsNoTracking()
            .OrderBy(f => f.CreatedAt)
            .Where(f => f.OwnerId == currentUser.UserId)
            .Where(predicate)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<FileAsset> GetFileAssetByIdAsync(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        return await dbContext.FileAssets
                   .AsNoTracking()
                   .SingleOrDefaultAsync(f => f.Id == fileAssetId && f.OwnerId == currentUser.UserId!, cancellationToken) ??
               throw new FileAssetNotFoundException(fileAssetId.ToString());
    }

    public async Task UpdateFileAssetAsync(FileAsset fileAsset, CancellationToken cancellationToken)
    {
        dbContext.FileAssets.Update(fileAsset);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<long> CountFileAssetsAsync(Expression<Func<FileAsset, bool>> predicate, CancellationToken cancellationToken)
    {
        return await dbContext.FileAssets
            .Where(n => n.OwnerId == currentUser.UserId)
            .Where(predicate)
            .LongCountAsync(cancellationToken);
    }

    public async Task DeleteFileAsset(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var fileAssetToDelete = await dbContext.FileAssets
            .FirstAsync(f => f.Id == fileAssetId, cancellationToken);

        fileAssetToDelete.MarkAsDeleted();

        dbContext.FileAssets.Update(fileAssetToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteFileAssets(IEnumerable<FileAssetId> fileAssetIds, CancellationToken cancellationToken)
    {
        var fileAssetsToDelete = await dbContext.FileAssets
            .Where(f => fileAssetIds.Contains(f.Id))
            .ToListAsync(cancellationToken);

        foreach (var fileAsset in fileAssetsToDelete)
            fileAsset.MarkAsDeleted();

        dbContext.FileAssets.UpdateRange(fileAssetsToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteFileAssetsByNodeIds(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        foreach (var nodeId in nodeIds)
        {
            var fileAssetsToDelete = await dbContext.FileAssets
                .Where(n => n.NodeId == nodeId)
                .ToListAsync(cancellationToken);

            foreach (var fileAsset in fileAssetsToDelete)
                fileAsset.MarkAsDeleted();

            dbContext.FileAssets.UpdateRange(fileAssetsToDelete);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task ReviveFileAsset(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var fileAssetToDelete = await dbContext.FileAssets
            .FirstAsync(f => f.Id == fileAssetId, cancellationToken);

        fileAssetToDelete.Revive();

        dbContext.FileAssets.Update(fileAssetToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<FileAsset>> GetFileAssetsBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        return await dbContext.FileAssets
            .AsNoTracking()
            .Where(f => nodeIds.Contains(f.NodeId) && !f.IsDeleted)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
