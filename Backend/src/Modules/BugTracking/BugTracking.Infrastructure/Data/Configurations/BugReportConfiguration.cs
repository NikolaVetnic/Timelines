using BugTracking.Domain.Models;
using BuildingBlocks.Domain.BugTracking.BugReport.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTracking.Infrastructure.Data.Configurations;

public class BugReportConfiguration : IEntityTypeConfiguration<BugReport>
{
    public void Configure(EntityTypeBuilder<BugReport> builder)
    {
        builder.HasKey(br => br.Id);
        builder.Property(br => br.Id).HasConversion(
            bugReportId => bugReportId.Value,
            dbId => BugReportId.Of(dbId));

        builder.Property(br => br.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(br => br.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(br => br.ReporterName)
            .IsRequired()
            .HasMaxLength(100);
    }
}