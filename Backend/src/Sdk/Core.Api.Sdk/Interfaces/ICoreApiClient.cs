using SdkNodeId = Core.Api.Sdk.Contracts.Nodes.ValueObjects.NodeId;
using SdkNodeDto = Core.Api.Sdk.Contracts.Nodes.Dtos.NodeDto;
using SdkCreateNodeRequest = Core.Api.Sdk.Contracts.Nodes.Commands.CreateNodeRequest;
using SdkCreateNodeResponse = Core.Api.Sdk.Contracts.Nodes.Commands.CreateNodeResponse;
using SdkGetNodeByIdResponse = Core.Api.Sdk.Contracts.Nodes.Queries.GetNodeByIdResponse;

namespace Core.Api.Sdk.Interfaces;

public interface ICoreApiClient
{
    Task<(SdkCreateNodeResponse? Response, HttpResponseMessage RawResponse)> CreateNodeAsync(SdkCreateNodeRequest request);
    Task<(SdkGetNodeByIdResponse? Response, HttpResponseMessage RawResponse)> GetNodeByIdAsync(SdkNodeId nodeId);
}
