// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

using Core.Api.Sdk.Contracts.Nodes.ValueObjects;

namespace Core.Api.Sdk.Contracts.Nodes.Commands;

public class CreateNodeResponse
{
    public required NodeId Id { get; init; }
}
