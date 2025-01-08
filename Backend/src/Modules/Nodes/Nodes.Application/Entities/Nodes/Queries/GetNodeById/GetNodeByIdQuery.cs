// ReSharper disable ClassNeverInstantiated.Global
namespace Nodes.Application.Entities.Nodes.Queries.GetNodeById;

public record GetNodeByIdQuery(string Id) : IQuery<GetNodeByIdResult>;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record GetNodeByIdResult(NodeDto NodeDto);
