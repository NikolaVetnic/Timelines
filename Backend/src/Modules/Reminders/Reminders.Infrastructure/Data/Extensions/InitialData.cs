namespace Reminders.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Reminder> Reminders =>
        new List<Reminder>
        {
            Reminder.Create(
                ReminderId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97")),
                "Meeting Room 1 - Daily", "Important meeting with Timo", DateTime.UtcNow, "High", DateTime.Now, "Pending"),

            Reminder.Create(
                ReminderId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970")),
                "Weekly Team Sync", "Basic team sync with the team", DateTime.Now, "Low", DateTime.Now, "Canceled"),
        };
}
