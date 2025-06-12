using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.Dtos;

namespace Nodes.Application.Entities.Nodes.Queries.ListFlaggedForDeletionNodes;

public record ListFlaggedForDeletionNodesQuery(PaginationRequest PaginationRequest) : IQuery<ListFlaggedForDeletionNodesResult>;

public record ListFlaggedForDeletionNodesResult(PaginatedResult<NodeDto> Nodes);
