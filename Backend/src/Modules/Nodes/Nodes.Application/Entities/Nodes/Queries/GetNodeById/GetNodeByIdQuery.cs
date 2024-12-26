// ReSharper disable ClassNeverInstantiated.Global

using Nodes.Application.Entities.Nodes.Dtos;

namespace Nodes.Application.Entities.Nodes.Queries.GetNodeById;

public record GetNodeByIdQuery(string Id) : IQuery<GetNodeByIdResult>;

public record GetNodeByIdResult(NodeDto NodeDto);
