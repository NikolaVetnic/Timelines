using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Notes.Infrastructure.Data.Configuration;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).HasConversion(
            nodeId => nodeId.Value,
            dbId => NoteId.Of(dbId));
        builder.Property(n => n.Title);
        builder.Property(n => n.Content);
        builder.Property(n => n.Timestamp);
        builder.Property(n => n.OwnerId);
        builder.Property(n => n.RelatedNotes);
        builder.Property(n => n.SharedWith);
        builder.Property(n => n.IsPublic);
    }
}
