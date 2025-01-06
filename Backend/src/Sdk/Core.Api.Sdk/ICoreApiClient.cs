using BuildingBlocks.Domain.ValueObjects.Ids;
using Nodes.Api.Endpoints.Nodes;
using Nodes.Application.Entities.Nodes.Dtos;

namespace Core.Api.Sdk;

public interface ICoreApiClient
{
    Task<(NodeId? NodeId, HttpResponseMessage RawResponse)> CreateNodeAsync(CreateNodeRequest request);
    Task<(NodeDto? NodeDto, HttpResponseMessage RawResponse)> GetNodeByIdAsync(NodeId nodeId);
}
