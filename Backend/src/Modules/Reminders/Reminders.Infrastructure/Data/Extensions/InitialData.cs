﻿namespace Reminders.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Reminder> Reminders =>
        new List<Reminder>
        {
            Reminder.Create(
                ReminderId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97")),
                "Meeting Room 1 - Daily"),

            Reminder.Create(
                ReminderId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970")),
                "Weekly Team Sync"),
        };
}