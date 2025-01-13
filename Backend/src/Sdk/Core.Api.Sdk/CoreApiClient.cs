using System.Net.Http.Json;
using Core.Api.Sdk.Interfaces;
using Mapster;
using ApiCreateNodeRequest = Nodes.Api.Endpoints.Nodes.CreateNodeRequest;
using ApiCreateNodeResponse = Nodes.Api.Endpoints.Nodes.CreateNodeResponse;
using ApiGetNodeByIdResponse = Nodes.Api.Endpoints.Nodes.GetNodeByIdResponse;
using SdkNodeId = Core.Api.Sdk.Contracts.Nodes.ValueObjects.NodeId;
using SdkCreateNodeRequest = Core.Api.Sdk.Contracts.Nodes.Commands.CreateNodeRequest;
using SdkCreateNodeResponse = Core.Api.Sdk.Contracts.Nodes.Commands.CreateNodeResponse;
using SdkGetNodeByIdResponse = Core.Api.Sdk.Contracts.Nodes.Queries.GetNodeByIdResponse;

namespace Core.Api.Sdk;

public class CoreApiClient(HttpClient httpClient) : ICoreApiClient
{
    public async Task<(SdkCreateNodeResponse? Response, HttpResponseMessage RawResponse)> CreateNodeAsync(
        SdkCreateNodeRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var apiRequest = request.Adapt<ApiCreateNodeRequest>();
        var response = await httpClient.PostAsJsonAsync("/nodes", apiRequest);

        if (!response.IsSuccessStatusCode)
            return (null, response);

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiCreateNodeResponse>();
        var sdkCreateNodeResponse = apiResponse.Adapt<SdkCreateNodeResponse>();

        return (sdkCreateNodeResponse, response);
    }

    public async Task<(SdkGetNodeByIdResponse? Response, HttpResponseMessage RawResponse)> GetNodeByIdAsync(
        SdkNodeId nodeId)
    {
        if (nodeId == null)
            throw new ArgumentNullException(nameof(nodeId));

        var response = await httpClient.GetAsync($"/nodes/{nodeId.Value}");

        if (!response.IsSuccessStatusCode)
            return (null, response);

        var apiNode = await response.Content.ReadFromJsonAsync<ApiGetNodeByIdResponse>();
        var sdkNode = apiNode.Adapt<SdkGetNodeByIdResponse>();

        return (sdkNode, response);
    }
}
