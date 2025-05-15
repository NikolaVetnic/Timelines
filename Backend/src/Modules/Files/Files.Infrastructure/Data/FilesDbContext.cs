using System.Reflection;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using Files.Application.Data.Abstractions;

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

            entity.HasKey(f => f.Id);
            entity.Property(f => f.Name);
            entity.Property(f => f.Size);
            entity.Property(f => f.Type);
            entity.Property(f => f.OwnerId);
            entity.Property(f => f.Description);

            entity.Property(r => r.NodeId).IsRequired();
            entity.HasIndex(r => r.NodeId); // Add an index for efficient querying

            // Configure NodeId with the value converter
            entity.Property(r => r.NodeId)
                .HasConversion(new NodeIdValueConverter()) // Apply the value converter
                .IsRequired();
        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
