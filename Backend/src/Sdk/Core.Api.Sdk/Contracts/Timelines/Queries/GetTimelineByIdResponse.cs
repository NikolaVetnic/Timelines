// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

using Core.Api.Sdk.Contracts.Timelines.Dtos;

namespace Core.Api.Sdk.Contracts.Timelines.Queries;

public class GetTimelineByIdResponse
{
    public required TimelineDto TimelineDto { get; init; }
}
