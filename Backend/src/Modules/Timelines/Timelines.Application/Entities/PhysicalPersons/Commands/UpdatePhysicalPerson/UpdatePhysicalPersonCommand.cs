using BuildingBlocks.Domain.Timelines.PhysicalPerson.Dtos;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;

namespace Timelines.Application.Entities.PhysicalPersons.Commands.UpdatePhysicalPerson;

public class UpdatePhysicalPersonCommand : ICommand<UpdatePhysicalPersonResult>
{
    public required PhysicalPersonId Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? ParentName { get; set; }

    public DateOnly? BirthDate { get; set; }
    public string? StreetAddress { get; set; } 
    public string? PersonalIdNumber { get; set; }
    public string? IdCardNumber { get; set; }

    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }

    public string? BankAccountNumber { get; set; }

    public string? Comment { get; set; }
}

public record UpdatePhysicalPersonResult(PhysicalPersonBaseDto PhysicalPerson);

public class UpdatePhysicalPersonCommandValidator : AbstractValidator<UpdatePhysicalPersonCommand>
{
    public UpdatePhysicalPersonCommandValidator()
    {
    }
}
