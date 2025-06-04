using System.Collections.Generic;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.Dtos;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;

namespace Timelines.Api.Controllers.PhysicalPersons;

public record CreatePhysicalPersonResponse(PhysicalPersonId Id);

public record GetPhysicalPersonByIdResponse(PhysicalPersonDto PhysicalPerson);

public record ListPhysicalPersonsResponse(IEnumerable<PhysicalPersonDto> PhysicalPersons);

public record UpdatePhysicalPersonResponse(PhysicalPersonBaseDto PhysicalPerson);

public record DeletePhysicalPersonResponse(bool PhysicalPersonDeleted);
