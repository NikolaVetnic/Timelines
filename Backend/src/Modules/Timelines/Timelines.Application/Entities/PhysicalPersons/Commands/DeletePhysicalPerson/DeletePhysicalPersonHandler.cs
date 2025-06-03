using Timelines.Application.Data.Abstractions;

namespace Timelines.Application.Entities.PhysicalPersons.Commands.DeletePhysicalPerson;

internal class DeletePhysicalPersonHandler(IPhysicalPersonsRepository physicalPersonsRepository)
    : ICommandHandler<DeletePhysicalPersonCommand, DeletePhysicalPersonResult>
{
    public async Task<DeletePhysicalPersonResult> Handle(DeletePhysicalPersonCommand command, CancellationToken cancellationToken)
    {
        await physicalPersonsRepository.GetPhysicalPersonByIdAsync(command.Id, cancellationToken);
        await physicalPersonsRepository.DeletePhysicalPersonAsync(command.Id, cancellationToken);

        return new DeletePhysicalPersonResult(true);
    }
}
