// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

using Core.Api.Sdk.Contracts.Timelines.Dtos;

namespace Core.Api.Sdk.Contracts.Timelines.Commands;

public class CreateTimelineRequest
{
    public required TimelineDto Timeline { get; init; }
}
