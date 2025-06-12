using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Application.Entities.PhysicalPersons.Commands.CreatePhysicalPerson;

public class CreatePhysicalPersonCommand : ICommand<CreatePhysicalPersonResult>
{
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
    public required string ParentName { get; set; }

    public required DateOnly BirthDate { get; set; }
    public required string StreetAddress { get; set; }
    public required string PersonalIdNumber { get; set; }
    public required string IdCardNumber { get; set; }

    public required string EmailAddress { get; set; }
    public required string PhoneNumber { get; set; }

    public string? BankAccountNumber { get; set; }

    public string? Comment { get; set; }

    public required TimelineId TimelineId { get; set; }
}

public record CreatePhysicalPersonResult(PhysicalPersonId Id);

public class CreatePhysicalPersonCommandValidator : AbstractValidator<CreatePhysicalPersonCommand>
{
    public CreatePhysicalPersonCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 100 characters.");
    }
}
