﻿namespace Notes.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Note> Notes =>
        new List<Note>
        {
            Note.Create(
                NoteId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97")), // use different ids in both cases
                "Check time!",
                "Check for the exact time of the Court Proceedings.",
                DateTime.UtcNow,
                0
            ),

            Note.Create(
                NoteId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970")),
                "Additional documents",
                "Make sure you bring both folders.",
                DateTime.UtcNow,
                1
            )
        };
}