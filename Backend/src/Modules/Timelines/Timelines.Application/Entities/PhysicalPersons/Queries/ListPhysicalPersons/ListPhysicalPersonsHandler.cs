using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.Dtos;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.PhysicalPersons.Extensions;

namespace Timelines.Application.Entities.PhysicalPersons.Queries.ListPhysicalPersons;

internal class ListPhysicalPersonsHandler(IPhysicalPersonsRepository physicalPersonsRepository, ITimelinesService timelinesService)
    : IQueryHandler<ListPhysicalPersonsQuery, ListPhysicalPersonsResult>
{
    public async Task<ListPhysicalPersonsResult> Handle(ListPhysicalPersonsQuery query, CancellationToken cancellationToken)
    {
        var physicalPersons = await physicalPersonsRepository.ListPhysicalPersonsAsync(query.TimelineId, cancellationToken);
        var timeline = await timelinesService.GetTimelineBaseByIdAsync(physicalPersons[0].TimelineId, cancellationToken);
        
        var physicalPersonDtos = physicalPersons.Select(p => p.ToPhysicalPersonDto(timeline)).ToList();

        return new ListPhysicalPersonsResult(physicalPersonDtos);
    }
}
