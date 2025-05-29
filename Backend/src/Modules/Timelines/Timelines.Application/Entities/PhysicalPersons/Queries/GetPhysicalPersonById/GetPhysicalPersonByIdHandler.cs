using BuildingBlocks.Application.Data;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.PhysicalPersons.Exceptions;
using Timelines.Application.Entities.PhysicalPersons.Extensions;
using Timelines.Application.Entities.Timelines.Exceptions;

namespace Timelines.Application.Entities.PhysicalPersons.Queries.GetPhysicalPersonById;

internal class GetPhysicalPersonByIdHandler(IPhysicalPersonsRepository physicalPersonsRepository, ITimelinesService timelinesService)
    : IQueryHandler<GetPhysicalPersonByIdQuery, GetPhysicalPersonByIdResult>
{
    public async Task<GetPhysicalPersonByIdResult> Handle(GetPhysicalPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var physicalPerson = await physicalPersonsRepository.GetPhysicalPersonByIdAsync(request.Id, cancellationToken);

        if (physicalPerson is null)
            throw new PhysicalPersonNotFoundException(request.Id.ToString());
        
        var timeline = await timelinesService.GetTimelineByIdAsync(physicalPerson.TimelineId, cancellationToken);
        
        if (timeline is null)
            throw new TimelineNotFoundException(physicalPerson.TimelineId.ToString());

        return new GetPhysicalPersonByIdResult(physicalPerson.ToPhysicalPersonDto(timeline));
    }
}
