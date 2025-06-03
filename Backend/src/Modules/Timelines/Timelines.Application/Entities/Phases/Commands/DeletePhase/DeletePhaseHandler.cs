using Timelines.Application.Data.Abstractions;

namespace Timelines.Application.Entities.Phases.Commands.DeletePhase;

internal class DeletePhaseHandler(IPhasesRepository phasesRepository)
    : ICommandHandler<DeletePhaseCommand, DeletePhaseResult>
{
    public async Task<DeletePhaseResult> Handle(DeletePhaseCommand command, CancellationToken cancellationToken)
    {
        var phase = await phasesRepository.GetPhaseByIdAsync(command.Id, cancellationToken);
        await phasesRepository.DeletePhaseAsync(phase.Id, cancellationToken);

        return new DeletePhaseResult(true);
    }
}
