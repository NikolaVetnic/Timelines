using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Files.File.Dtos;
using Files.Application.Entities.Files.Queries.ListFIleAssetsByNodeId;

namespace Nodes.Api.Endpoints.Nodes;

public class ListFileAssetsByNodeId : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Nodes/{nodeId}/Files", async (string nodeId, [AsParameters] PaginationRequest query, ISender sender) =>
            {
                var result = await sender.Send(new ListFileAssetsByNodeIdQuery(nodeId, query));
                var response = result.Adapt<ListFileAssetsByNodeIdResponse>();
                return Results.Ok(response);
            })
            .WithName("ListFileAssetsByNodeId")
            .Produces<ListFileAssetsByNodeIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("List File Assets by Node Id")
            .WithDescription("List File Assets by Node Id");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record ListFileAssetsByNodeIdResponse(PaginatedResult<FileAssetBaseDto> FileAssets);