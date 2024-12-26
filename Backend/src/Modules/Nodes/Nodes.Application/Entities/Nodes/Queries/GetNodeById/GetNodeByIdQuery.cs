// ReSharper disable ClassNeverInstantiated.Global
namespace Nodes.Application.Nodes.Queries.GetNodeById;

public record GetNodeByIdQuery(string Id) : IQuery<GetNodeByIdResult>;

public record GetNodeByIdResult(NodeDto NodeDto);
