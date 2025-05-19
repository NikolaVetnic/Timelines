using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.Phase.Dtos;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global

namespace Timelines.Application.Entities.Phases.Queries.ListPhases;

public record ListPhasesQuery(PaginationRequest PaginationRequest) : IQuery<ListPhasesResult>;

public record ListPhasesResult(PaginatedResult<PhaseDto> Phases);
