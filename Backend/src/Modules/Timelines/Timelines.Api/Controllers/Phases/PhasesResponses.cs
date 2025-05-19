using BuildingBlocks.Domain.Timelines.Phase.Dtos;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;

namespace Timelines.Api.Controllers.Phases;

public record CreatePhaseResponse(PhaseId Id);

public record GetPhaseByIdResponse(PhaseDto Phase);
