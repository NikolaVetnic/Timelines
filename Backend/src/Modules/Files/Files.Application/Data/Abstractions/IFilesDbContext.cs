namespace Files.Application.Data.Abstractions;

public interface IFilesDbContext
{
    DbSet<FileAsset> FileAssets { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
