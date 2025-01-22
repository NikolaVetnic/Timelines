using SdkTimelineId = Core.Api.Sdk.Contracts.Timelines.ValueObjects.TimelineId;
using SdkTimelineDto = Core.Api.Sdk.Contracts.Timelines.Dtos.TimelineDto;
using SdkCreateTimelineRequest = Core.Api.Sdk.Contracts.Timelines.Commands.CreateTimelineRequest;

// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable Reqnroll.MethodNameMismatchPattern

namespace Timelines.Integration.StepDefinitions;

[Binding]
public class TimelinesApiStepDefinitions
{
    private readonly ICoreApiClient _apiApiClient;
    private HttpResponseMessage? _response;

    private SdkTimelineDto? _timelineDto;
    private SdkTimelineId? _persistedTimelineId;

    public TimelinesApiStepDefinitions(ICoreApiClient apiClient)
    {
        _apiApiClient = apiClient;
    }

    #region When

    [When("a POST request is sent to the /Timelines endpoint with a valid payload")]
    public async Task WhenAPostRequestIsSentToTheTimelineEndpoint()
    {
        var (response, rawResponse) =
            await _apiApiClient.CreateTimelineAsync(new SdkCreateTimelineRequest
            {
                Timeline = new SdkTimelineDto
                {
                    Id = null,
                    Title = "Submit Annual Tax Return",
                }
            });

        (_response = rawResponse).EnsureSuccessStatusCode();
        rawResponse.EnsureSuccessStatusCode();

        (_persistedTimelineId = response!.Id).Should().NotBeNull();
    }

    #endregion

    #region Then

    [Then(@"the response status code is (\d{3}) \((.+)\)")]
    public void ThenTheResponseStatusCodeIs(int expectedStatusCode, string description)
    {
        if (_response != null)
            ((int)_response.StatusCode).Should().Be(expectedStatusCode);
    }

    [Then("the Timeline is created")]
    public async Task ThenTheTimelineIsCreated()
    {
        var (response, rawResponse) = await _apiApiClient.GetTimelineByIdAsync(_persistedTimelineId!);

        rawResponse.EnsureSuccessStatusCode();
        var timelineDto = response!.TimelineDto;

        timelineDto?.Title.Should().Be("Submit Annual Tax Return");
    }

    #endregion
}
