using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.Dtos;

namespace Timelines.Application.Entities.PhysicalPersons.Queries.ListPhysicalPersons;

public record ListPhysicalPersonsQuery(PaginationRequest PaginationRequest) : IQuery<ListPhysicalPersonsResult>;

public record ListPhysicalPersonsResult(PaginatedResult<PhysicalPersonDto> PhysicalPersons);
