using BuildingBlocks.Application.Pagination;
using Nodes.Application.Entities.Nodes.Dtos;

// ReSharper disable ClassNeverInstantiated.Global

namespace Nodes.Application.Entities.Nodes.Queries.ListNodes;

public record ListNodesQuery(PaginationRequest PaginationRequest) : IQuery<ListNodesResult>;

public record ListNodesResult(PaginatedResult<NodeDto> Nodes);
