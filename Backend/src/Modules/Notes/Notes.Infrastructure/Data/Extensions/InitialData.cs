namespace Notes.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Note> Notes =>
        new List<Note>
        {
            Note.Create(
                NoteId.Of(Guid.Parse("d562b07b-ea65-45d4-958d-4e890391cbc7")),
                "Check time!",
                "Check for the exact time of the Court Proceedings.",
                DateTime.UtcNow,
                0
            ),

            Note.Create(
                NoteId.Of(Guid.Parse("d562b07b-ea65-45d4-958d-4e890391cbc7")),
                "Additional documents",
                "Make sure you bring both folders.",
                DateTime.UtcNow,
                1
            )
        };
}
