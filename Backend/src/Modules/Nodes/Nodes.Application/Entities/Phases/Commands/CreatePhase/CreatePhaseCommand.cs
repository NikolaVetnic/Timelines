using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;

namespace Nodes.Application.Entities.Phases.Commands.CreatePhase;

// ReSharper disable once ClassNeverInstantiated.Global
public record CreatePhaseCommand : ICommand<CreatePhaseResult>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required TimeSpan Duration { get; set; }
    public required string Status { get; set; }
    public required decimal Progress { get; set; }
    public required bool IsCompleted { get; set; }
    public required PhaseId Parent { get; set; }
    public required List<PhaseId> DependsOn { get; set; }
    public required string AssignedTo { get; set; }
    public required List<string> Stakeholders { get; set; } = [];
    public required List<string> Tags { get; set; } = [];

}

public record CreatePhaseResult(PhaseId Id);

public class CreatePhaseCommandValidator : AbstractValidator<CreatePhaseCommand>
{
    public CreatePhaseCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
    }
}