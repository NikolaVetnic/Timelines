using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Nodes.Phase.Dtos;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Nodes.Application.Data.Abstractions;

namespace Nodes.Application.Data;

public class PhasesService(IPhasesRepository phasesRepository, IServiceProvider serviceProvider) : IPhasesService
{
    public async Task<PhaseDto> GetPhaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken)
    {
        var phase = await phasesRepository.GetPhaseByIdAsync(phaseId, cancellationToken);
        var phaseDto = phase.Adapt<PhaseDto>();

        var nodesService = serviceProvider.GetRequiredService<INodesService>();

        foreach (var nodeId in phase.NodeIds)
        {
            var node = await nodesService.GetNodeBaseByIdAsync(nodeId, cancellationToken);
            phaseDto.Nodes.Add(node);
        }

        return phaseDto;
    }

    public async Task<PhaseBaseDto> GetPhaseBaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken)
    {
        var phase = await phasesRepository.GetPhaseByIdAsync(phaseId, cancellationToken);
        var phaseBaseDto = phase.Adapt<PhaseBaseDto>();

        return phaseBaseDto;
    }

    public async Task AddNode(PhaseId phaseId, NodeId nodeId, CancellationToken cancellationToken)
    {
        var phase = await phasesRepository.GetPhaseByIdAsync(phaseId, cancellationToken);
        phase.

        await phasesRepository.UpdatePhaseAsync(phase, cancellationToken);
    }
}
