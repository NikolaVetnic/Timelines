using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;
using Timelines.Application.Data.Abstractions;

namespace Timelines.Application.Entities.PhysicalPersons.Commands.CreatePhysicalPerson;

public class CreatePhysicalPersonHandler(ICurrentUser currentUser, IPhysicalPersonsRepository physicalPersonsRepository, ITimelinesService timelinesService) : ICommandHandler<CreatePhysicalPersonCommand, CreatePhysicalPersonResult>
{
    public async Task<CreatePhysicalPersonResult> Handle(CreatePhysicalPersonCommand command, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId!;
        var physicalPerson = command.ToPhysicalPerson(userId);
        
        await timelinesService.EnsureTimelineBelongsToOwner(physicalPerson.TimelineId, cancellationToken);
        await physicalPersonsRepository.CreatePhysicalPersonAsync(physicalPerson, cancellationToken);
        await timelinesService.AddPhysicalPerson(physicalPerson.TimelineId, physicalPerson.Id, cancellationToken);

        return new CreatePhysicalPersonResult(physicalPerson.Id);
    }
}

internal static class CreatePhysicalPersonHandlerExtensions
{
    public static PhysicalPerson ToPhysicalPerson(this CreatePhysicalPersonCommand command, string userId)
    {
        return PhysicalPerson.Create(
            PhysicalPersonId.Of(Guid.NewGuid()),
            command.FirstName,
            command.MiddleName,
            command.LastName,
            command.ParentName,
            command.BirthDate,
            command.StreetAddress,
            command.PersonalIdNumber,
            command.IdCardNumber,
            command.EmailAddress,
            command.PhoneNumber,
            command.BankAccountNumber,
            command.Comment,
            userId,
            command.TimelineId
        );
    }
}
