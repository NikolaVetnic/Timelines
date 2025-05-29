using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;

namespace Timelines.Application.Entities.PhysicalPersons.Commands.DeletePhysicalPerson;

public record DeletePhysicalPersonCommand(PhysicalPersonId Id) : ICommand<DeletePhysicalPersonResult>
{
    public DeletePhysicalPersonCommand(string id) : this(PhysicalPersonId.Of(Guid.Parse(id))) { }
}

public record DeletePhysicalPersonResult(bool PhysicalPersonDeleted);

public class DeletePhysicalPersonCommandValidator : AbstractValidator<DeletePhysicalPersonCommand>
{
    public DeletePhysicalPersonCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
