using System.Globalization;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Nodes.Api.Endpoints.Nodes;
using Nodes.Application.Entities.Nodes.Dtos;
using NodeId = BuildingBlocks.Domain.ValueObjects.Ids.NodeId;

// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable Reqnroll.MethodNameMismatchPattern

namespace Core.Test.StepDefinitions;

[Binding]
public class NodesApiStepDefinitions
{
    private readonly HttpClient _client;
    private HttpResponseMessage? _response;

    private NodeDto? _nodeDto;
    private NodeId? _persistedNodeId;

    public NodesApiStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    #region Given

    [Given("a Node")]
    public void GivenANode()
    {
        var dateTime = DateTime.Parse("2024-01-15T09:00:00.000000Z", null, DateTimeStyles.RoundtripKind);

        _nodeDto = new NodeDto(null, "Test Node", "Test Node Description.", dateTime, 1, "Testing",
            ["Category1", "Category2"], ["Tag1", "Tag2", "Tag3"]
        );
    }

    #endregion

    #region When

    [When("a POST request is sent to the /Nodes endpoint")]
    public async Task WhenAPostRequestIsSentToTheNodesEndpoint()
    {
        _response = await _client.PostAsJsonAsync("/Nodes", new CreateNodeRequest(_nodeDto));
        _response.EnsureSuccessStatusCode();
        
        _persistedNodeId = await _response.Content.ReadFromJsonAsync<NodeId>();
    }

    #endregion

    #region Then

    [Then(@"the response status code is (\d{3}) \((.+)\)")]
    public void ThenTheResponseStatusCodeIs(int expectedStatusCode, string description)
    {
        if (_response != null) ((int)_response.StatusCode).Should().Be(expectedStatusCode);
    }

    [Then("the Node is created")]
    public async Task ThenTheNodeIsCreated()
    {
        var getResponse = await _client.GetAsync($"/Nodes/{_persistedNodeId}");
        var getNodeByIdResponse = await getResponse.Content.ReadFromJsonAsync<GetNodeByIdResponse>();
        var nodeDto = getNodeByIdResponse?.NodeDto;
        
        nodeDto?.Id
            .Should().Be(_persistedNodeId?.ToString());
        nodeDto?.Title
            .Should().Be("Test Node");
        nodeDto?.Description
            .Should().Be("Test Node Description.");
        nodeDto?.Timestamp
            .Should().Be(DateTime.Parse("2024-01-15T09:00:00.000000Z", null, DateTimeStyles.RoundtripKind));
        nodeDto?.Importance
            .Should().Be(1);
        nodeDto?.Phase
            .Should().Be("Testing");
    }

    #endregion
}
