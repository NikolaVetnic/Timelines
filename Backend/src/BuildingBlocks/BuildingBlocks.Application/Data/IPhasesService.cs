using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Nodes.Phase.Dtos;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface IPhasesService
{
    Task<PhaseDto> GetPhaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken);
    Task<PhaseBaseDto> GetPhaseBaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken);
    Task AddNode(PhaseId phaseId, NodeId nodeId, CancellationToken cancellationToken);
}
