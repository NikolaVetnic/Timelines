using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;

namespace Reminders.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Reminder> Reminders =>
        new List<Reminder>
        {
            new()
            {
                Id = ReminderId.Of(Guid.Parse("2e1c4902-7841-484c-b997-0a8cd3955e72")),
                Title = "Meeting Room 1 - Daily",
                Description = "Important meeting with Timo",
                NotifyAt = DateTime.UtcNow.AddHours(2),
                Priority = 3,
                ColorHex = "#007bff",
                OwnerId = "11111111-1111-1111-1111-111111111111",
                NodeId = NodeId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97"))
            },

            new()
            {
                Id = ReminderId.Of(Guid.Parse("74f40a78-bda2-4177-bffd-86e6648c4318")),
                Title = "Weekly Team Sync",
                Description = "Basic team sync with the team",
                NotifyAt = DateTime.UtcNow.AddHours(2),
                Priority = 1,
                ColorHex = "#e76a29",
                OwnerId = "22222222-2222-2222-2222-222222222222",
                NodeId = NodeId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970"))
            },
        };
}
