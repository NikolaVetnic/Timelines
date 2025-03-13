using BuildingBlocks.Domain.Files.File.ValueObjects;
using Files.Application.Data.Abstractions;
using Files.Application.Entities.Files.Exceptions;

namespace Files.Infrastructure.Data.Repositories;

public class FilesRepository(IFilesDbContext dbContext) : IFilesRepository
{
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
}
