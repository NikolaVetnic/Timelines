using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.Phase.Dtos;

namespace Timelines.Application.Entities.Phases.Queries.ListFlaggedForDeletionPhases;

public record ListFlaggedForDeletionPhasesQuery(PaginationRequest PaginationRequest) : IQuery<ListFlaggedForDeletionPhasesResult>;

public record ListFlaggedForDeletionPhasesResult(PaginatedResult<PhaseDto> Phases);
