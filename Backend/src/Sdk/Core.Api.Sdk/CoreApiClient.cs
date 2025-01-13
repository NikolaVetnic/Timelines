using System.Net.Http.Json;
using Core.Api.Sdk.Interfaces;

namespace Core.Api.Sdk;

public class CoreApiClient(HttpClient httpClient) : ICoreApiClient
{
    public async Task<(NodeId? NodeId, HttpResponseMessage RawResponse)> CreateNodeAsync(CreateNodeRequest request)
    {
        var response = await httpClient.PostAsJsonAsync("/Nodes", request);
        var nodeId = await response.Content.ReadFromJsonAsync<NodeId>();
        
        return (nodeId, response);
    }

    public async Task<(NodeDto? NodeDto, HttpResponseMessage RawResponse)> GetNodeByIdAsync(NodeId nodeId)
    {
        var response = await httpClient.GetAsync($"/Nodes/{nodeId}");
        var result = await response.Content.ReadFromJsonAsync<GetNodeByIdResponse>();
        
        return (result?.NodeDto, response);
    }
}
