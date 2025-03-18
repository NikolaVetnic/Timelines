using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;
using Nodes.Application.Data.Abstractions;

namespace Nodes.Application.Entities.Phases.Commands.CreatePhase;

internal class CreatePhaseHandler(IPhasesDbContext dbContext) : ICommandHandler<CreatePhaseCommand, CreatePhaseResult>
{
    public async Task<CreatePhaseResult> Handle(CreatePhaseCommand command, CancellationToken cancellationToken)
    {
        var phase = command.ToPhase();

        dbContext.Phases.Add(phase);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreatePhaseResult(phase.Id);
    }
}

internal static class CreatePhaseCommandExtensions
{
    public static Phase ToPhase(this CreatePhaseCommand command)
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
            command.Tags
        );
    }
}
