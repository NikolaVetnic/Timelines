using System.Globalization;
using Core.Api.Sdk;
using CreateNodeRequest = Nodes.Api.Endpoints.Nodes.CreateNodeRequest;
using NodeDto = Nodes.Application.Entities.Nodes.Dtos.NodeDto;
using NodeId = BuildingBlocks.Domain.ValueObjects.Ids.NodeId;

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

    private NodeDto? _nodeDto;
    private NodeId? _persistedNodeId;

    public NodesApiStepDefinitions(ICoreApiClient apiClient)
    {
        _apiApiClient = apiClient;
    }

    #region Given

    [Given("a Node")]
    public void GivenANode()
    {
        _nodeDto = new NodeDto(
            null, 
            "Test Node", 
            "Test Node Description.",
            DateTime.Parse("2024-01-15T09:00:00.000000Z", null, DateTimeStyles.RoundtripKind), 
            1, 
            "Testing",
            ["Category1", "Category2"], 
            ["Tag1", "Tag2", "Tag3"]
        );
    }

    #endregion

    #region When

    [When("a POST request is sent to the /Nodes endpoint")]
    public async Task WhenAPostRequestIsSentToTheNodesEndpoint()
    {
        (_persistedNodeId, _response) = await _apiApiClient.CreateNodeAsync(new CreateNodeRequest(_nodeDto));

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
        var (nodeDto, response) = await _apiApiClient.GetNodeByIdAsync(_persistedNodeId!);
        
        response.EnsureSuccessStatusCode();

        nodeDto?.Id.Should().Be(_persistedNodeId?.ToString());
        nodeDto?.Title.Should().Be(_nodeDto!.Title);
        nodeDto?.Description.Should().Be(_nodeDto!.Description);
        nodeDto?.Timestamp.Should().Be(_nodeDto!.Timestamp);
        nodeDto?.Importance.Should().Be(_nodeDto!.Importance);
        nodeDto?.Phase.Should().Be(_nodeDto!.Phase);
    }

    #endregion
}
