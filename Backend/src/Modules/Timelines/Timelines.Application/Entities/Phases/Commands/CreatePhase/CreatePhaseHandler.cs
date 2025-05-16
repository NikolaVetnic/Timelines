using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using Timelines.Application.Data.Abstractions;

namespace Timelines.Application.Entities.Phases.Commands.CreatePhase;

internal class CreatePhaseHandler(ICurrentUser currentUser, IPhasesRepository phasesRepository, ITimelinesService timelinesService) : ICommandHandler<CreatePhaseCommand, CreatePhaseResult>
{
    public async Task<CreatePhaseResult> Handle(CreatePhaseCommand command, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId!;
        var phase = command.ToPhase(userId);

        await phasesRepository.CreatePhaseAsync(phase, cancellationToken);
        await timelinesService.AddPhase(phase.TimelineId, phase.Id, cancellationToken);

        return new CreatePhaseResult(phase.Id);
    }
}

internal static class CreatePhaseCommandExtensions
{
    public static Phase ToPhase(this CreatePhaseCommand command, string userId)
    {
        return Phase.Create(
            PhaseId.Of(Guid.NewGuid()),
            command.Title,
            command.Description,
            command.StartDate,
            command.EndDate,
            command.Duration,
            command.Status,
            command.Progress,
            command.IsCompleted,
            command.Parent,
            command.DependsOn,
            command.AssignedTo,
            command.Stakeholders,
            command.Tags,
            command.TimelineId,
            userId
        );
    }
}
