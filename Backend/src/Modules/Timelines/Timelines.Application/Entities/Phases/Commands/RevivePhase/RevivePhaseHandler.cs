using Timelines.Application.Data.Abstractions;

namespace Timelines.Application.Entities.Phases.Commands.RevivePhase;

internal class RevivePhaseHandler(IPhasesRepository phasesRepository) : ICommandHandler<RevivePhaseCommand, RevivePhaseResult>
{
    public async Task<RevivePhaseResult> Handle(RevivePhaseCommand command, CancellationToken cancellationToken)
    {
        var phase = await phasesRepository.GetPhaseByIdAsync(command.Id, cancellationToken);
        await phasesRepository.DeletePhaseAsync(phase.Id, cancellationToken);

        return new RevivePhaseResult(true);
    }
}
