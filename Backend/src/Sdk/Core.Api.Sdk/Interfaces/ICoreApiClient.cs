using SdkNodeId = Core.Api.Sdk.Contracts.Nodes.ValueObjects.NodeId;
using SdkCreateNodeRequest = Core.Api.Sdk.Contracts.Nodes.Commands.CreateNodeRequest;
using SdkCreateNodeResponse = Core.Api.Sdk.Contracts.Nodes.Commands.CreateNodeResponse;
using SdkGetNodeByIdResponse = Core.Api.Sdk.Contracts.Nodes.Queries.GetNodeByIdResponse;
using SdkCreateNoteRequest = Core.Api.Sdk.Contracts.Notes.Commands.CreateNoteRequest;
using SdkCreateNoteResponse = Core.Api.Sdk.Contracts.Notes.Commands.CreateNoteResponse;

namespace Core.Api.Sdk.Interfaces;

public interface ICoreApiClient
{
    Task<(SdkCreateNodeResponse? Response, HttpResponseMessage RawResponse)> CreateNodeAsync(SdkCreateNodeRequest request);
    Task<(SdkGetNodeByIdResponse? Response, HttpResponseMessage RawResponse)> GetNodeByIdAsync(SdkNodeId nodeId);
    Task<(SdkCreateNoteResponse? Response, HttpResponseMessage RawResponse)> CreateNoteAsync(SdkCreateNoteRequest request);
}
