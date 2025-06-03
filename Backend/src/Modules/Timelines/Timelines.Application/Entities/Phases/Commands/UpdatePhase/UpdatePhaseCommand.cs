using BuildingBlocks.Domain.Timelines.Phase.Dtos;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Application.Entities.Phases.Commands.UpdatePhase;

public record UpdatePhaseCommand : ICommand<UpdatePhaseResult>
{
    public required PhaseId Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public TimeSpan? Duration { get; set; }
    public string? Status { get; set; }
    public decimal? Progress { get; set; }
    public bool? IsCompleted { get; set; }
    public PhaseId[]? DependsOn { get; set; }
    public string? AssignedTo { get; set; }
    public string[]? Stakeholders { get; set; }
    public string[]? Tags { get; set; }
    public TimelineId? TimelineId { get; set; }
}

// ReSharper disable once NotAccessedPositionalProperty.Global

public record UpdatePhaseResult(PhaseBaseDto Phase);

public class UpdatePhaseCommandValidator : AbstractValidator<UpdatePhaseCommand>
{
    public UpdatePhaseCommandValidator()
    {
    }
}
