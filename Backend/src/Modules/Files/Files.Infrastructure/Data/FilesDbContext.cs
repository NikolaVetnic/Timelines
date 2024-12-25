using Files.Application.Data;
using System.Reflection;

namespace Files.Infrastructure.Data;

public class FilesDbContext(DbContextOptions<FilesDbContext> options) :
    DbContext(options), IFilesDbContext
{
    public DbSet<FileAsset> FileAssets { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Specify schema for this module
        builder.HasDefaultSchema("Files");

        // Configure entities
        builder.Entity<FileAsset>(entity =>
        {
            entity.ToTable("Files"); // Specify table name within the schema
        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
