using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
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
                "Alice",
                ["Bob", "Carol"],
                false,
                NodeId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97"))
            ),

            Note.Create(
                NoteId.Of(Guid.Parse("dffbedcb-b793-4ac2-8767-1fb391033644")),
                "Additional documents",
                "Make sure you bring both folders.",
                DateTime.UtcNow,
                "Dagmar",
                ["Eivor","Feofan"],
                true,
                NodeId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970"))
            )
        };
}
