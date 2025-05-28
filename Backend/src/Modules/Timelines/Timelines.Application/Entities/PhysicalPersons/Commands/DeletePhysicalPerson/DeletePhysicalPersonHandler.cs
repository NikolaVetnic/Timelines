using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.PhysicalPersons.Exceptions;

namespace Timelines.Application.Entities.PhysicalPersons.Commands.DeletePhysicalPerson;

public class DeletePhysicalPersonHandler(IPhysicalPersonsRepository physicalPersonsRepository) : ICommandHandler<DeletePhysicalPersonCommand, DeletePhysicalPersonResult>
{
    public async Task<DeletePhysicalPersonResult> Handle(DeletePhysicalPersonCommand command, CancellationToken cancellationToken)
    {
        var physicalPerson = await physicalPersonsRepository.GetPhysicalPersonByIdAsync(command.Id, cancellationToken);

        if (physicalPerson is null)
            throw new PhysicalPersonNotFoundException(command.Id.ToString());
        
        await physicalPersonsRepository.DeletePhysicalPersonAsync(command.Id, cancellationToken);

        return new DeletePhysicalPersonResult(true);
    }
}
