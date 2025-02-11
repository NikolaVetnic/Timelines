using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace Notes.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Note> Notes =>
        new List<Note>
        {
            Note.Create(
                NoteId.Of(Guid.Parse("74dad71c-4ddc-4d4d-a894-3307ddc3fe10")),
                "Check time!",
                "Check for the exact time of the Court Proceedings.",
                DateTime.UtcNow,
                "Timo",
                ["Michael", "Dirk"],
                false
            ),

            Note.Create(
                NoteId.Of(Guid.Parse("dffbedcb-b793-4ac2-8767-1fb391033644")),
                "Additional documents",
                "Make sure you bring both folders.",
                DateTime.UtcNow,
                "Thomas",
                ["Daniel","Claus"],
                true
            )
        };
}
