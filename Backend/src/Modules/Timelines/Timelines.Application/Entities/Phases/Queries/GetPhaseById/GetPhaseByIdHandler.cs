using BuildingBlocks.Application.Data;
using Timelines.Application.Entities.Phases.Exceptions;

namespace Timelines.Application.Entities.Phases.Queries.GetPhaseById;

internal class GetPhaseByIdHandler(IPhasesService phasesService)
    : IQueryHandler<GetPhaseByIdQuery, GetPhaseByIdResult>
{
    public async Task<GetPhaseByIdResult> Handle(GetPhaseByIdQuery query, CancellationToken cancellationToken)
    {
        var phaseDto = await phasesService.GetPhaseByIdAsync(query.Id, cancellationToken);

        if (phaseDto is null)
            throw new PhaseNotFoundException(query.Id.ToString());

        return new GetPhaseByIdResult(phaseDto);
    }
}
