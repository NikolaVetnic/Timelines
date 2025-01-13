using SdkNodeId = Core.Api.Sdk.Contracts.Nodes.ValueObjects.NodeId;
using SdkNodeDto = Core.Api.Sdk.Contracts.Nodes.Dtos.NodeDto;
using SdkCreateNodeRequest = Core.Api.Sdk.Contracts.Nodes.Commands.CreateNodeRequest;

// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable Reqnroll.MethodNameMismatchPattern

namespace Core.Test.StepDefinitions;

[Binding]
public class NodesApiStepDefinitions
{
    private readonly ICoreApiClient _apiApiClient;
    private HttpResponseMessage? _response;

    private SdkNodeDto? _nodeDto;
    private SdkNodeId? _persistedNodeId;

    public NodesApiStepDefinitions(ICoreApiClient apiClient)
    {
        _apiApiClient = apiClient;
    }

    #region When

    [When("a POST request is sent to the /Nodes endpoint with a valid payload")]
    public async Task WhenAPostRequestIsSentToTheNodesEndpoint()
    {
        var (response, rawResponse) =
            await _apiApiClient.CreateNodeAsync(new SdkCreateNodeRequest
            {
                Node = new SdkNodeDto
                {
                    Id = null,
                    Title = "Test Node",
                    Description = "Test Node Description.",
                    Timestamp = DateTime.Parse("2024-01-15T09:00:00.000000Z", null, DateTimeStyles.RoundtripKind),
                    Importance = 1,
                    Phase = "Testing",
                    Categories = ["Category1", "Category2"],
                    Tags = ["Tag1", "Tag2", "Tag3"]
                }
            });

        rawResponse.EnsureSuccessStatusCode();

        _persistedNodeId = response!.Id;
        _response = rawResponse;

        _persistedNodeId.Should().NotBeNull();
        _response.EnsureSuccessStatusCode();
    }

    #endregion

    #region Then

    [Then(@"the response status code is (\d{3}) \((.+)\)")]
    public void ThenTheResponseStatusCodeIs(int expectedStatusCode, string description)
    {
        if (_response != null)
            ((int)_response.StatusCode).Should().Be(expectedStatusCode);
    }

    [Then("the Node is created")]
    public async Task ThenTheNodeIsCreated()
    {
        var (response, rawResponse) = await _apiApiClient.GetNodeByIdAsync(_persistedNodeId!);

        rawResponse.EnsureSuccessStatusCode();

        var nodeDto = response!.NodeDto;

        nodeDto?.Title.Should().Be("Test Node");
        nodeDto?.Description.Should().Be("Test Node Description.");
        nodeDto?.Timestamp.Should().Be(DateTime.Parse("2024-01-15T09:00:00.000000Z", null, DateTimeStyles.RoundtripKind));
        nodeDto?.Importance.Should().Be(1);
        nodeDto?.Phase.Should().Be("Testing");
    }

    #endregion
}
