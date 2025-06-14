﻿using BuildingBlocks.Domain.Files.File.ValueObjects;
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

        builder.Property(n => n.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(n => n.Size)
            .IsRequired();

        builder.Property(n => n.Type)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(n => n.Description)
            .IsRequired()
            .HasMaxLength(500);
    }
}
