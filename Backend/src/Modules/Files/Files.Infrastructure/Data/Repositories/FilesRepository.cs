using BuildingBlocks.Domain.Files.File.ValueObjects;
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

    public async Task DeleteFileAsset(FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var fileAssetToDelete = await dbContext.FileAssets
            .FirstAsync(f => f.Id == fileAssetId, cancellationToken);

        dbContext.FileAssets.Remove(fileAssetToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
