using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Application.Data.Abstractions;

public interface IPhysicalPersonsRepository
{
    Task CreatePhysicalPersonAsync(PhysicalPerson phase, CancellationToken cancellationToken);

    Task<List<PhysicalPerson>> ListPhysicalPersonsPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<PhysicalPerson> GetPhysicalPersonByIdAsync(PhysicalPersonId phaseId, CancellationToken cancellationToken);
    Task<List<PhysicalPerson>> GetPhysicalPersonsByIdsAsync(IEnumerable<PhysicalPersonId> phaseIds, CancellationToken cancellationToken);

    Task<IEnumerable<PhysicalPerson>> GetPhysicalPersonsBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken);
    Task<long> PhysicalPersonCountAsync(CancellationToken cancellationToken);

    Task UpdatePhysicalPersonAsync(PhysicalPerson phase, CancellationToken cancellationToken);
    Task DeletePhysicalPersonAsync(PhysicalPersonId phaseId, CancellationToken cancellationToken);
    Task DeletePhysicalPersons(IEnumerable<PhysicalPersonId> phaseIds, CancellationToken cancellationToken);


}
