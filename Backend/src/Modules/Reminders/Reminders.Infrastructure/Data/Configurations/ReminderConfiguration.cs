using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reminders.Infrastructure.Data.Configurations;

public class ReminderConfiguration : IEntityTypeConfiguration<Reminder>
{
    public void Configure(EntityTypeBuilder<Reminder> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasConversion(
            reminderId => reminderId.Value,
            dbId => ReminderId.Of(dbId));

        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(r => r.NotifyAt)
            .IsRequired();

        builder.Property(r => r.Priority)
            .IsRequired();
    }
}
