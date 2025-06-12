using BuildingBlocks.Application.Data;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.PhysicalPersons.Exceptions;
using Timelines.Application.Entities.PhysicalPersons.Extensions;

namespace Timelines.Application.Entities.PhysicalPersons.Commands.UpdatePhysicalPerson;

internal class UpdatePhysicalPersonHandler(IPhysicalPersonsRepository physicalPersonsRepository, ITimelinesService timelinesService)
    : ICommandHandler<UpdatePhysicalPersonCommand, UpdatePhysicalPersonResult>
{
    public async Task<UpdatePhysicalPersonResult> Handle(UpdatePhysicalPersonCommand command, CancellationToken cancellationToken)
    {
        var physicalPerson = await physicalPersonsRepository.GetPhysicalPersonByIdAsync(command.Id, cancellationToken);

        if (physicalPerson is null)
            throw new PhysicalPersonNotFoundException(command.Id.ToString());

        physicalPerson.FirstName = command.FirstName ?? physicalPerson.FirstName;
        physicalPerson.MiddleName = command.MiddleName ?? physicalPerson.MiddleName;
        physicalPerson.LastName = command.LastName ?? physicalPerson.LastName;
        physicalPerson.ParentName = command.ParentName ?? physicalPerson.ParentName;

        physicalPerson.BirthDate = command.BirthDate ?? physicalPerson.BirthDate;
        physicalPerson.StreetAddress = command.StreetAddress ?? physicalPerson.StreetAddress;
        physicalPerson.PersonalIdNumber = command.PersonalIdNumber ?? physicalPerson.PersonalIdNumber;
        physicalPerson.IdCardNumber = command.IdCardNumber ?? physicalPerson.IdCardNumber;

        physicalPerson.EmailAddress = command.EmailAddress ?? physicalPerson.EmailAddress;
        physicalPerson.PhoneNumber = command.PhoneNumber ?? physicalPerson.PhoneNumber;

        physicalPerson.BankAccountNumber = command.BankAccountNumber ?? physicalPerson.BankAccountNumber;

        physicalPerson.Comment = command.Comment ?? physicalPerson.Comment;

        var timeline = await timelinesService.GetTimelineByIdAsync(physicalPerson.TimelineId, cancellationToken);

        if (timeline.Id is null)
            throw new NotFoundException(
                $"Related timeline with ID {physicalPerson.TimelineId} not found");

        await physicalPersonsRepository.UpdatePhysicalPersonAsync(physicalPerson, cancellationToken);

        return new UpdatePhysicalPersonResult(physicalPerson.ToPhysicalPersonBaseDto());
    }
}
