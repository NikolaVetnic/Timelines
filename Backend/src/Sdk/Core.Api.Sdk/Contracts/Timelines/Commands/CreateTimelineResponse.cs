// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

using Core.Api.Sdk.Contracts.Timelines.ValueObjects;

namespace Core.Api.Sdk.Contracts.Timelines.Commands;

public class CreateTimelineResponse
{
    public required TimelineId Id { get; init; }
}
