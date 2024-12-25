using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Files.Infrastructure.Data.Configurations;

public class FileAssetConfiguration : IEntityTypeConfiguration<FileAsset>
{
    public void Configure(EntityTypeBuilder<FileAsset> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).HasConversion(
            fileAssetId => fileAssetId.Value,
            dbId => FileAssetId.Of(dbId));

        // ToDo: Add remaining FileAsset configuration commands
    }
}
