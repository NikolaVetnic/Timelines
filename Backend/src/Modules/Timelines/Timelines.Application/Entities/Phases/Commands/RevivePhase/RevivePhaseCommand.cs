using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;

namespace Timelines.Application.Entities.Phases.Commands.RevivePhase;

public record RevivePhaseCommand(PhaseId Id) : ICommand<RevivePhaseResult>
{
    public RevivePhaseCommand(string Id) : this(PhaseId.Of(Guid.Parse(Id))) { }
}

public record RevivePhaseResult(bool ReviveResult);

public class RevivePhaseCommandValidator : AbstractValidator<RevivePhaseCommand>
{
    public RevivePhaseCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
