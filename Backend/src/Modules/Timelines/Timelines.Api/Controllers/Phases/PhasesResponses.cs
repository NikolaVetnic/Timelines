using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Timelines.Phase.Dtos;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;

namespace Timelines.Api.Controllers.Phases;

public record CreatePhaseResponse(PhaseId Id);

public record GetPhaseByIdResponse(PhaseDto Phase);

public record ListPhasesResponse(PaginatedResult<PhaseDto> Phases);

public record ListNodesByPhaseIdResponse(PaginatedResult<NodeBaseDto> Nodes);

public record UpdatePhaseResponse(PhaseBaseDto Phase);

public record DeletePhaseResponse(bool PhaseDeleted);
