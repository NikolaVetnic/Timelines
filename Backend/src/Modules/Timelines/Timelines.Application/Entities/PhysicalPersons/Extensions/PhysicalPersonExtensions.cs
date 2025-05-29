using BuildingBlocks.Domain.Timelines.PhysicalPerson.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

namespace Timelines.Application.Entities.PhysicalPersons.Extensions;

public static class PhysicalPersonExtensions
{
    public static PhysicalPersonDto ToPhysicalPersonDto(this PhysicalPerson physicalPerson, TimelineBaseDto timeline)
    {
        return new PhysicalPersonDto(
            physicalPerson.Id.ToString(),
            physicalPerson.FirstName,
            physicalPerson.MiddleName,
            physicalPerson.LastName,
            physicalPerson.ParentName,
            physicalPerson.BirthDate,
            physicalPerson.StreetAddress,
            physicalPerson.PersonalIdNumber,
            physicalPerson.IdCardNumber,
            physicalPerson.EmailAddress,
            physicalPerson.PhoneNumber,
            physicalPerson.BankAccountNumber,
            physicalPerson.Comment,
            timeline,
            physicalPerson.OwnerId
        );
    }

    public static PhysicalPersonBaseDto ToPhysicalPersonBaseDto(this PhysicalPerson physicalPerson)
    {
        return new PhysicalPersonBaseDto(
            physicalPerson.Id.ToString(),
            physicalPerson.FirstName,
            physicalPerson.MiddleName,
            physicalPerson.LastName,
            physicalPerson.ParentName,
            physicalPerson.BirthDate,
            physicalPerson.StreetAddress,
            physicalPerson.PersonalIdNumber,
            physicalPerson.IdCardNumber,
            physicalPerson.EmailAddress,
            physicalPerson.PhoneNumber,
            physicalPerson.BankAccountNumber,
            physicalPerson.Comment,
            physicalPerson.OwnerId
        );
    }
}
