using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace Notes.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Note> Notes =>
        new List<Note>
        {
            new()
            {
                Id = NoteId.Of(Guid.Parse("74dad71c-4ddc-4d4d-a894-3307ddc3fe10")),
                Title = "Check time!",
                Content = "Check for the exact time of the Court Proceedings.",
                Timestamp = DateTime.UtcNow,
                OwnerId = "11111111-1111-1111-1111-111111111111",
                SharedWith = ["Bob", "Carol"],
                IsPublic = false,
                NodeId = NodeId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97"))
            },

            new()
            {
                Id = NoteId.Of(Guid.Parse("dffbedcb-b793-4ac2-8767-1fb391033644")),
                Title = "Additional documents",
                Content = "Make sure you bring both folders.",
                Timestamp = DateTime.UtcNow,
                OwnerId = "22222222-2222-2222-2222-222222222222",
                SharedWith = ["Eivor","Feofan"],
                IsPublic = true,
                NodeId = NodeId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970"))
            }
        };
}
