using BuildingBlocks.Domain;
using Nodes.Domain.ValueObjects.Ids;

namespace Nodes.Domain.Models;

public class Phase : Entity<PhaseId>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime? EndDate { get; set; }
    public required TimeSpan? Duration { get; set; }
    public required string Status { get; set; }
    public required decimal Progress { get; set; }
    public required bool IsCompleted { get; set; }
    public required PhaseId Parent { get; set; }
    public required PhaseId[] DependsOn { get; set; }
    public required string AssignedTo { get; set; }
    public required string[] Stakeholders { get; set; }
    public required string[] Tags { get; set; }
}
