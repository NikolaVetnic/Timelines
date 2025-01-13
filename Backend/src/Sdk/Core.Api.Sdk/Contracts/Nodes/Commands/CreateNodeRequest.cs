// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

using Core.Api.Sdk.Contracts.Nodes.Dtos;

namespace Core.Api.Sdk.Contracts.Nodes.Commands;

public class CreateNodeRequest
{
    public required NodeDto Node { get; init; }
}
