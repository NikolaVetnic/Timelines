﻿using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace BuildingBlocks.Api.Converters;

public class NodeIdConverter : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<NodeId, NodeId>().ConstructUsing(src => NodeId.Of(src.Value));
}
