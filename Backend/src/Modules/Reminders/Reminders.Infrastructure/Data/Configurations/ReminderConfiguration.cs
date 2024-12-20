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

        // ToDO: Add remaining Reminder configuration commands
    }
}
