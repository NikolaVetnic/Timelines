// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Core.Api.Sdk.Contracts.Nodes.ValueObjects;

public class NodeId
{
    public NodeId() { }
    
    public NodeId(Guid value) => Value = value;

    public Guid Value { get; init; }
}
