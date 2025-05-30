using BuildingBlocks.Application.Data;
using Timelines.Application.Entities.Phases.Exceptions;

namespace Timelines.Application.Entities.Phases.Commands.DeletePhase;

internal class DeletePhaseHandler(IPhasesService phasesService)
    : ICommandHandler<DeletePhaseCommand, DeletePhaseResult>
{
    public async Task<DeletePhaseResult> Handle(DeletePhaseCommand command, CancellationToken cancellationToken)
    {
        var phase = await phasesService.GetPhaseBaseByIdAsync(command.Id, cancellationToken);

        if (phase is null)
            throw new PhaseNotFoundException(command.Id.ToString());

        await phasesService.DeletePhase(command.Id, cancellationToken);

        return new DeletePhaseResult(true);
    }
}
