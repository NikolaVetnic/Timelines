namespace Files.Application.Data;

public interface IFilesDbContext
{
    DbSet<FileAsset> FileAssets { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
