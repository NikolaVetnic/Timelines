using System.Net.Http.Json;
using Mapster;
using ApiCreateTimelineRequest = Timelines.Api.Controllers.Timelines.CreateTimelineRequest;
using ApiCreateTimelineResponse = Timelines.Api.Controllers.Timelines.CreateTimelineResponse;
using ApiGetTimelineByIdResponse = Timelines.Api.Controllers.Timelines.GetTimelineByIdResponse;
using SdkTimelineId = Core.Api.Sdk.Contracts.Timelines.ValueObjects.TimelineId;
using SdkCreateTimelineRequest = Core.Api.Sdk.Contracts.Timelines.Commands.CreateTimelineRequest;
using SdkCreateTimelineResponse = Core.Api.Sdk.Contracts.Timelines.Commands.CreateTimelineResponse;
using SdkGetTimelineByIdResponse = Core.Api.Sdk.Contracts.Timelines.Queries.GetTimelineByIdResponse;

namespace Core.Api.Sdk;

public partial class CoreApiClient
{
    public async Task<(SdkCreateTimelineResponse? Response, HttpResponseMessage RawResponse)> CreateTimelineAsync(
        SdkCreateTimelineRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var apiRequest = request.Adapt<ApiCreateTimelineRequest>();
        var response = await httpClient.PostAsJsonAsync("/timelines", apiRequest);

        if (!response.IsSuccessStatusCode)
            return (null, response);

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiCreateTimelineResponse>();
        var sdkCreateTimelineResponse = apiResponse.Adapt<SdkCreateTimelineResponse>();

        return (sdkCreateTimelineResponse, response);
    }

    public async Task<(SdkGetTimelineByIdResponse? Response, HttpResponseMessage RawResponse)> GetTimelineByIdAsync(
        SdkTimelineId timelineId)
    {
        if (timelineId == null)
            throw new ArgumentNullException(nameof(timelineId));

        var response = await httpClient.GetAsync($"/timelines/{timelineId.Value}");

        if (!response.IsSuccessStatusCode)
            return (null, response);

        var apiTimeline = await response.Content.ReadFromJsonAsync<ApiGetTimelineByIdResponse>();
        var sdkTimeline = apiTimeline.Adapt<SdkGetTimelineByIdResponse>();

        return (sdkTimeline, response);
    }
}
