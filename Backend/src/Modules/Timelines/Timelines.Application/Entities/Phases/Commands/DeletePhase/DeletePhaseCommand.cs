using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;

namespace Timelines.Application.Entities.Phases.Commands.DeletePhase;

public record DeletePhaseCommand(PhaseId Id) : ICommand<DeletePhaseResult>
{
    public DeletePhaseCommand(string Id) : this(PhaseId.Of(Guid.Parse(Id))) { }

}

public record DeletePhaseResult(bool PhaseDeleted);

public class DeletePhaseCommandValidator : AbstractValidator<DeletePhaseCommand>
{
    public DeletePhaseCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
