using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;

namespace Nodes.Application.Entities.Phases.Commands.CreatePhase;

public class CreatePhaseCommand : ICommand<CreatePhaseResult>
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
    public required PhaseId[] DependsOn { get; set; }
    public required string AssignedTo { get; set; }
    public required string[] Stakeholders { get; set; }
    public required string[] Tags { get; set; }
}

public record CreatePhaseResult(PhaseId Id);

public class CreatePhaseCommandValidator: AbstractValidator<CreatePhaseCommand>
{
    public CreatePhaseCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.Status).NotEmpty();
        RuleFor(x => x.Progress).NotEmpty();
        RuleFor(x => x.IsCompleted).NotEmpty();
        RuleFor(x => x.Parent).NotEmpty();
        RuleFor(x => x.DependsOn).NotEmpty();
        RuleFor(x => x.AssignedTo).NotEmpty();
        RuleFor(x => x.Stakeholders).NotEmpty();
        RuleFor(x => x.Tags).NotEmpty();
    }
}
