using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Application.Entities.PhysicalPersons.Queries.ListPhysicalPersons;

public record ListPhysicalPersonsQuery(TimelineId TimelineId) : IQuery<ListPhysicalPersonsResult>;

public record ListPhysicalPersonsResult(IEnumerable<PhysicalPersonDto> PhysicalPersons);
