using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.Dtos;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global

namespace Nodes.Application.Entities.Nodes.Queries.ListNodes;

public record ListNodesQuery(PaginationRequest PaginationRequest) : IQuery<ListNodesResult>;

public record ListNodesResult(PaginatedResult<NodeDto> Nodes);
