// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

using Core.Api.Sdk.Contracts.Nodes.Dtos;

namespace Core.Api.Sdk.Contracts.Nodes.Queries;

public class GetNodeByIdResponse
{
    public required NodeDto NodeDto { get; init; }
}
