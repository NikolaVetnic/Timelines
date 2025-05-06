namespace Nodes.Application.Data.Abstractions.Phases;

public interface IPhasesRepository
{
    Task CreatePhaseAsync(Phase phase, CancellationToken cancellationToken);
}