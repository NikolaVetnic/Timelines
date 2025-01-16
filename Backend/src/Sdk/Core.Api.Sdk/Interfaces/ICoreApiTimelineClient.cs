using SdkTimelineId = Core.Api.Sdk.Contracts.Timelines.ValueObjects.TimelineId;
using SdkCreateTimelineRequest = Core.Api.Sdk.Contracts.Timelines.Commands.CreateTimelineRequest;
using SdkCreateTimelineResponse = Core.Api.Sdk.Contracts.Timelines.Commands.CreateTimelineResponse;
using SdkGetTimelineByIdResponse = Core.Api.Sdk.Contracts.Timelines.Queries.GetTimelineByIdResponse;

namespace Core.Api.Sdk.Interfaces;

public partial interface ICoreApiClient
{
    Task<(SdkCreateTimelineResponse? Response, HttpResponseMessage RawResponse)> CreateTimelineAsync(SdkCreateTimelineRequest request);

    Task<(SdkGetTimelineByIdResponse? Response, HttpResponseMessage RawResponse)> GetTimelineByIdAsync(SdkTimelineId timelineId);
}
