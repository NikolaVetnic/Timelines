using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Notes.Infrastructure.Data.Configuration;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).HasConversion(
            noteId => noteId.Value,
            dbId => NoteId.Of(dbId));

        builder.Property(n => n.Title)
            .IsRequired();

        builder.Property(n => n.Content)
            .IsRequired();

        builder.Property(n => n.IsPublic)
            .IsRequired();
    }
}
