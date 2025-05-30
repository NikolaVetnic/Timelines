using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Timeline> Timelines =>
        new List<Timeline>
        {
            new()
            {
                Id = TimelineId.Of(Guid.Parse("d87c1c54-b287-4a82-b45b-65db1d71e2fd")),
                Title = "Timeline 1",
                Description = "Timeline 1 description",
                OwnerId = "11111111-1111-1111-1111-111111111111",
                NodeIds = [NodeId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97"))],
                PhaseIds = [PhaseId.Of(Guid.Parse("b6facdf1-5a3c-4521-9e60-8f9a24e3ad3b"))],
            },
            new()
            {
                Id = TimelineId.Of(Guid.Parse("f739b2c7-883e-46c6-917c-d29114e3e017")),
                Title = "Timeline 2",
                Description = "Timeline 2 description",
                OwnerId = "22222222-2222-2222-2222-222222222222",
                NodeIds = [NodeId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970"))],
                PhaseIds = [PhaseId.Of(Guid.Parse("f1801bf8-78a5-4417-bd7c-0397094fcb05"))],
            }
        };

    public static IEnumerable<Phase> Phases =>
        new List<Phase>
        {
            new()
            {
                Id = PhaseId.Of(Guid.Parse("b6facdf1-5a3c-4521-9e60-8f9a24e3ad3b")),
                Title = "Preparation",
                Description = "Preparation phase for the case.",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                Duration = TimeSpan.FromDays(30),
                Status = "In Progress",
                Progress = 0.5m,
                IsCompleted = false,
                Parent = PhaseId.Of(Guid.Parse("b6facdf1-5a3c-4521-9e60-8f9a24e3ad3b")),
                DependsOn = [],
                AssignedTo = "John Doe",
                Stakeholders = ["Jane Smith", "Bob Johnson"],
                Tags = ["court", "complaint"],
                TimelineId = TimelineId.Of(Guid.Parse("d87c1c54-b287-4a82-b45b-65db1d71e2fd")),
                OwnerId = "11111111-1111-1111-1111-111111111111",
            },
            new()
            {
                Id = PhaseId.Of(Guid.Parse("f1801bf8-78a5-4417-bd7c-0397094fcb05")),
                Title = "Investigation",
                Description = "Investigation phase for the case.",
                StartDate = DateTime.UtcNow.AddDays(31),
                EndDate = DateTime.UtcNow.AddDays(60),
                Duration = TimeSpan.FromDays(30),
                Status = "Not Started",
                Progress = 0.0m,
                IsCompleted = false,
                Parent = PhaseId.Of(Guid.Parse("b6facdf1-5a3c-4521-9e60-8f9a24e3ad3b")),
                DependsOn = [],
                AssignedTo = "Jane Smith",
                Stakeholders = ["John Doe", "Bob Johnson"],
                Tags = ["investigation", "interview"],
                TimelineId = TimelineId.Of(Guid.Parse("f739b2c7-883e-46c6-917c-d29114e3e017")),
                OwnerId = "22222222-2222-2222-2222-222222222222",
            }
        };

    public static IEnumerable<PhysicalPerson> PhysicalPersons =>
        new List<PhysicalPerson>
        {
            new()
            {
                Id = PhysicalPersonId.Of(Guid.Parse("ada74725-8434-428c-8ff4-dc9abeeee2b0")),
                FirstName = "Alice",
                MiddleName = "Emilia",
                LastName = "Alison",
                ParentName = "Andrew",
                BirthDate = DateOnly.Parse("12/12/1986"),
                StreetAddress = "Smith Street 57, Brussels",
                PersonalIdNumber = "1234567890123",
                IdCardNumber = "123456789",
                EmailAddress = "alice@alison.com",
                PhoneNumber = "+123456789",
                BankAccountNumber = "123456789",
                Comment = "My comment.",
                TimelineId = TimelineId.Of(Guid.Parse("d87c1c54-b287-4a82-b45b-65db1d71e2fd")),
                OwnerId = "11111111-1111-1111-1111-111111111111"
            },
            new()
            {
                Id = PhysicalPersonId.Of(Guid.Parse("56c28d98-d5ae-4d08-8306-82d719b30250")),
                FirstName = "Bob",
                MiddleName = "Frank",
                LastName = "Robertson",
                ParentName = "Barry",
                BirthDate = DateOnly.Parse("11/11/1982"),
                StreetAddress = "Baker Street 17, Amsterdam",
                PersonalIdNumber = "3210987654321",
                IdCardNumber = "987654321",
                EmailAddress = "bob@robertson.com",
                PhoneNumber = "+987654321",
                BankAccountNumber = "987654321",
                Comment = "Another comment.",
                TimelineId = TimelineId.Of(Guid.Parse("f739b2c7-883e-46c6-917c-d29114e3e017")),
                OwnerId = "22222222-2222-2222-2222-222222222222"
            }
        };
}
