using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.Dtos;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;

namespace Timelines.Application.Entities.PhysicalPersons.Queries.GetPhysicalPersonById;

public record GetPhysicalPersonByIdQuery(PhysicalPersonId Id) : IQuery<GetPhysicalPersonByIdResult>
{
    public GetPhysicalPersonByIdQuery(string id) : this(PhysicalPersonId.Of(Guid.Parse(id))) { }
}

public record GetPhysicalPersonByIdResult(PhysicalPersonDto PhysicalPerson);

public class GetPhysicalPersonByIdQueryValidator : AbstractValidator<GetPhysicalPersonByIdQuery>
{
    public GetPhysicalPersonByIdQueryValidator()
    {
        
    }
}
