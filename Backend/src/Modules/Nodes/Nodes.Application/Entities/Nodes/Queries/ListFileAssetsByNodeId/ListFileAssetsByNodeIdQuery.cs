using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

// ReSharper disable ClassNeverInstantiated.Global

namespace Nodes.Application.Entities.Nodes.Queries.ListFileAssetsByNodeId;

public record ListFileAssetsByNodeIdQuery(NodeId Id, PaginationRequest PaginationRequest) : IQuery<ListFileAssetsByNodeIdResult>
{
    public ListFileAssetsByNodeIdQuery(string id, PaginationRequest paginationRequest)
        : this(NodeId.Of(Guid.Parse(id)), paginationRequest) { }
}

public record ListFileAssetsByNodeIdResult(PaginatedResult<FileAssetBaseDto> FileAssets);
