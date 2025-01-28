using BuildingBlocks.Domain.Reminders.ValueObjects;

namespace Reminders.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Reminder> Reminders =>
        [
            Reminder.Create(
                ReminderId.Of(Guid.Parse("2e1c4902-7841-484c-b997-0a8cd3955e72")),
                "Meeting Room 1 - Daily",
                "Important meeting with Timo",
                DateTime.UtcNow.AddHours(2),
                3,
                DateTime.UtcNow.AddHours(1),
                "Pending",
                NodeId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97"))),

            Reminder.Create(
                ReminderId.Of(Guid.Parse("74f40a78-bda2-4177-bffd-86e6648c4318")),
                "Weekly Team Sync",
                "Basic team sync with the team",
                DateTime.UtcNow.AddHours(2),
                1,
                DateTime.UtcNow.AddHours(1),
                "Canceled",
                NodeId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970"))),
        ];
}
