// ReSharper disable ClassNeverInstantiated.Global

namespace Nodes.Application.Entities.Nodes.Queries.ListNodes;

public record ListNodesQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<ListNodesResult>;

public record ListNodesResult(IEnumerable<Node> Nodes);
