namespace Reminders.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Reminder> Reminders =>
        new List<Reminder>
        {
            Reminder.Create(
                ReminderId.Of(Guid.Parse("2e1c4902-7841-484c-b997-0a8cd3955e72")),
                "Meeting Room 1 - Daily",
                "Important meeting with Timo",
                DateTime.UtcNow.AddHours(2), 
                3, 
                DateTime.UtcNow.AddHours(1), 
                "Pending"),

            Reminder.Create(
                ReminderId.Of(Guid.Parse("74f40a78-bda2-4177-bffd-86e6648c4318")),
                "Weekly Team Sync",
                "Basic team sync with the team",
                DateTime.UtcNow.AddHours(2), 
                1,
                DateTime.UtcNow.AddHours(1),
                "Canceled"),
        };
}
