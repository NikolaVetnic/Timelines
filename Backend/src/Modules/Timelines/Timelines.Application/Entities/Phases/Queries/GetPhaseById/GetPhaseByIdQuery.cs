using BuildingBlocks.Domain.Timelines.Phase.Dtos;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;

namespace Timelines.Application.Entities.Phases.Queries.GetPhaseById;

public record GetPhaseByIdQuery(PhaseId Id) : IQuery<GetPhaseByIdResult>
{
    public GetPhaseByIdQuery(string Id) : this(PhaseId.Of(Guid.Parse(Id))) { }
}

public record GetPhaseByIdResult(PhaseDto Phase);

public class GetPhaseByIdQueryValidator : AbstractValidator<GetPhaseByIdQuery>
{
    public GetPhaseByIdQueryValidator()
    {
    }
}