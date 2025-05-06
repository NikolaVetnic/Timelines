using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;

namespace Nodes.Infrastructure.Data.Extensions.Phases;

internal static class InitialData
{
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
            Tags = ["court", "complaint"]
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
            Tags = ["investigation", "interview"]
        }
    };
}
